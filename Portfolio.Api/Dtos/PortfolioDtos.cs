namespace Portfolio.Api.Dtos;

public sealed record PortfolioResponse(
    ProfileDto Profile,
    IReadOnlyList<ExperienceDto> Experiences,
    IReadOnlyList<ProjectDto> Projects,
    IReadOnlyList<SkillCategoryDto> SkillCategories,
    IReadOnlyList<EducationDto> Education,
    IReadOnlyList<string> Certifications,
    IReadOnlyList<string> CoreSubjects);

public sealed record ProfileDto(
    string Name,
    string Title,
    string Summary,
    string Email,
    string Phone,
    string Location,
    string LinkedInUrl,
    string GitHubUrl);

public sealed record ExperienceDto(
    string Role,
    string Company,
    string Location,
    string StartDate,
    string EndDate,
    IReadOnlyList<string> Highlights);

public sealed record ProjectDto(
    string Name,
    string DateRange,
    string Summary,
    IReadOnlyList<string> Technologies,
    IReadOnlyList<string> Highlights);

public sealed record SkillCategoryDto(
    string Name,
    IReadOnlyList<string> Skills);

public sealed record EducationDto(
    string Degree,
    string Institution,
    string Location,
    string DateRange,
    string Result);

public sealed record ContactMessageRequest(
    string Name,
    string Email,
    string? Subject,
    string Message);
