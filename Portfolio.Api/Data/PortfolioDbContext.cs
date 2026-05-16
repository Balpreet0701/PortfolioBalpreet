using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Models;

namespace Portfolio.Api.Data;

public class PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : DbContext(options)
{
    public DbSet<Profile> Profiles => Set<Profile>();
    public DbSet<Experience> Experiences => Set<Experience>();
    public DbSet<ExperienceHighlight> ExperienceHighlights => Set<ExperienceHighlight>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectTechnology> ProjectTechnologies => Set<ProjectTechnology>();
    public DbSet<ProjectHighlight> ProjectHighlights => Set<ProjectHighlight>();
    public DbSet<SkillCategory> SkillCategories => Set<SkillCategory>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<EducationItem> Education => Set<EducationItem>();
    public DbSet<Certification> Certifications => Set<Certification>();
    public DbSet<CoreSubject> CoreSubjects => Set<CoreSubject>();
    public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Profile>(entity =>
        {
            entity.Property(item => item.Name).HasMaxLength(120).IsRequired();
            entity.Property(item => item.Title).HasMaxLength(160).IsRequired();
            entity.Property(item => item.Summary).HasMaxLength(1200).IsRequired();
            entity.Property(item => item.Email).HasMaxLength(160).IsRequired();
            entity.Property(item => item.Phone).HasMaxLength(40).IsRequired();
            entity.Property(item => item.Location).HasMaxLength(160).IsRequired();
            entity.Property(item => item.LinkedInUrl).HasMaxLength(300).IsRequired();
            entity.Property(item => item.GitHubUrl).HasMaxLength(300).IsRequired();
        });

        modelBuilder.Entity<Experience>(entity =>
        {
            entity.Property(item => item.Role).HasMaxLength(160).IsRequired();
            entity.Property(item => item.Company).HasMaxLength(160).IsRequired();
            entity.Property(item => item.Location).HasMaxLength(160).IsRequired();
            entity.Property(item => item.StartDate).HasMaxLength(40).IsRequired();
            entity.Property(item => item.EndDate).HasMaxLength(40).IsRequired();
        });

        modelBuilder.Entity<ExperienceHighlight>(entity =>
        {
            entity.Property(item => item.Text).HasMaxLength(700).IsRequired();
            entity.HasOne(item => item.Experience)
                .WithMany(item => item.Highlights)
                .HasForeignKey(item => item.ExperienceId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.Property(item => item.Name).HasMaxLength(180).IsRequired();
            entity.Property(item => item.DateRange).HasMaxLength(60).IsRequired();
            entity.Property(item => item.Summary).HasMaxLength(700).IsRequired();
        });

        modelBuilder.Entity<ProjectTechnology>(entity =>
        {
            entity.Property(item => item.Name).HasMaxLength(80).IsRequired();
            entity.HasOne(item => item.Project)
                .WithMany(item => item.Technologies)
                .HasForeignKey(item => item.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ProjectHighlight>(entity =>
        {
            entity.Property(item => item.Text).HasMaxLength(700).IsRequired();
            entity.HasOne(item => item.Project)
                .WithMany(item => item.Highlights)
                .HasForeignKey(item => item.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SkillCategory>(entity =>
        {
            entity.Property(item => item.Name).HasMaxLength(120).IsRequired();
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.Property(item => item.Name).HasMaxLength(80).IsRequired();
            entity.HasOne(item => item.SkillCategory)
                .WithMany(item => item.Skills)
                .HasForeignKey(item => item.SkillCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<EducationItem>(entity =>
        {
            entity.Property(item => item.Degree).HasMaxLength(160).IsRequired();
            entity.Property(item => item.Institution).HasMaxLength(220).IsRequired();
            entity.Property(item => item.Location).HasMaxLength(160).IsRequired();
            entity.Property(item => item.DateRange).HasMaxLength(60).IsRequired();
            entity.Property(item => item.Result).HasMaxLength(80).IsRequired();
        });

        modelBuilder.Entity<Certification>(entity =>
        {
            entity.Property(item => item.Name).HasMaxLength(180).IsRequired();
        });

        modelBuilder.Entity<CoreSubject>(entity =>
        {
            entity.Property(item => item.Name).HasMaxLength(120).IsRequired();
        });

        modelBuilder.Entity<ContactMessage>(entity =>
        {
            entity.Property(item => item.Name).HasMaxLength(120).IsRequired();
            entity.Property(item => item.Email).HasMaxLength(160).IsRequired();
            entity.Property(item => item.Subject).HasMaxLength(180).IsRequired();
            entity.Property(item => item.Message).HasMaxLength(2000).IsRequired();
        });
    }
}
