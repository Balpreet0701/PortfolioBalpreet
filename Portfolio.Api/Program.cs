using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Data;
using Portfolio.Api.Dtos;
using Portfolio.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
    ?? new[] { "http://localhost:5173", "http://localhost:5174" };

builder.Services.AddCors(options =>
{
    options.AddPolicy("PortfolioClient", policy =>
        policy.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();
    await db.Database.EnsureCreatedAsync();
    await PortfolioSeeder.SeedAsync(db);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PortfolioClient");

app.MapGet("/api/health", () => Results.Ok(new { status = "ok", service = "Balpreet Portfolio API" }))
    .WithName("Health");

app.MapGet("/api/portfolio", async (PortfolioDbContext db) =>
{
    var profile = await db.Profiles.AsNoTracking().FirstAsync();

    var experiences = await db.Experiences
        .AsNoTracking()
        .Include(experience => experience.Highlights)
        .OrderBy(experience => experience.SortOrder)
        .ToListAsync();

    var projects = await db.Projects
        .AsNoTracking()
        .Include(project => project.Technologies)
        .Include(project => project.Highlights)
        .OrderBy(project => project.SortOrder)
        .ToListAsync();

    var skillCategories = await db.SkillCategories
        .AsNoTracking()
        .Include(category => category.Skills)
        .OrderBy(category => category.SortOrder)
        .ToListAsync();

    var education = await db.Education
        .AsNoTracking()
        .OrderBy(item => item.SortOrder)
        .ToListAsync();

    var certifications = await db.Certifications
        .AsNoTracking()
        .OrderBy(item => item.SortOrder)
        .ToListAsync();

    var coreSubjects = await db.CoreSubjects
        .AsNoTracking()
        .OrderBy(item => item.SortOrder)
        .Select(item => item.Name)
        .ToListAsync();

    var response = new PortfolioResponse(
        new ProfileDto(
            profile.Name,
            profile.Title,
            profile.Summary,
            profile.Email,
            profile.Phone,
            profile.Location,
            profile.LinkedInUrl,
            profile.GitHubUrl),
        experiences.Select(experience => new ExperienceDto(
            experience.Role,
            experience.Company,
            experience.Location,
            experience.StartDate,
            experience.EndDate,
            experience.Highlights.OrderBy(highlight => highlight.SortOrder).Select(highlight => highlight.Text).ToList())).ToList(),
        projects.Select(project => new ProjectDto(
            project.Name,
            project.DateRange,
            project.Summary,
            project.Technologies.OrderBy(technology => technology.SortOrder).Select(technology => technology.Name).ToList(),
            project.Highlights.OrderBy(highlight => highlight.SortOrder).Select(highlight => highlight.Text).ToList())).ToList(),
        skillCategories.Select(category => new SkillCategoryDto(
            category.Name,
            category.Skills.OrderBy(skill => skill.SortOrder).Select(skill => skill.Name).ToList())).ToList(),
        education.Select(item => new EducationDto(
            item.Degree,
            item.Institution,
            item.Location,
            item.DateRange,
            item.Result)).ToList(),
        certifications.Select(item => item.Name).ToList(),
        coreSubjects);

    return Results.Ok(response);
})
.WithName("GetPortfolio");

app.MapPost("/api/contact", async ([FromBody] ContactMessageRequest request, PortfolioDbContext db) =>
{
    var errors = ValidateContactRequest(request);
    if (errors.Count > 0)
    {
        return Results.ValidationProblem(errors);
    }

    var message = new ContactMessage
    {
        Name = request.Name.Trim(),
        Email = request.Email.Trim(),
        Subject = string.IsNullOrWhiteSpace(request.Subject) ? "Portfolio inquiry" : request.Subject.Trim(),
        Message = request.Message.Trim(),
        CreatedUtc = DateTime.UtcNow
    };

    db.ContactMessages.Add(message);
    await db.SaveChangesAsync();

    return Results.Created($"/api/contact/{message.Id}", new
    {
        message.Id,
        message.CreatedUtc,
        note = "Message saved successfully."
    });
})
.WithName("CreateContactMessage");

app.Run();

static Dictionary<string, string[]> ValidateContactRequest(ContactMessageRequest request)
{
    var errors = new Dictionary<string, string[]>();

    if (string.IsNullOrWhiteSpace(request.Name))
    {
        errors["name"] = new[] { "Name is required." };
    }

    if (string.IsNullOrWhiteSpace(request.Email) || !request.Email.Contains('@'))
    {
        errors["email"] = new[] { "A valid email is required." };
    }

    if (string.IsNullOrWhiteSpace(request.Message))
    {
        errors["message"] = new[] { "Message is required." };
    }

    return errors;
}
