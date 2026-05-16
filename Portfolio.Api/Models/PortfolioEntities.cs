namespace Portfolio.Api.Models;

public class Profile
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string LinkedInUrl { get; set; } = string.Empty;
    public string GitHubUrl { get; set; } = string.Empty;
}

public class Experience
{
    public int Id { get; set; }
    public string Role { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public ICollection<ExperienceHighlight> Highlights { get; set; } = new List<ExperienceHighlight>();
}

public class ExperienceHighlight
{
    public int Id { get; set; }
    public int ExperienceId { get; set; }
    public string Text { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public Experience? Experience { get; set; }
}

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string DateRange { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public ICollection<ProjectTechnology> Technologies { get; set; } = new List<ProjectTechnology>();
    public ICollection<ProjectHighlight> Highlights { get; set; } = new List<ProjectHighlight>();
}

public class ProjectTechnology
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public Project? Project { get; set; }
}

public class ProjectHighlight
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string Text { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public Project? Project { get; set; }
}

public class SkillCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public ICollection<Skill> Skills { get; set; } = new List<Skill>();
}

public class Skill
{
    public int Id { get; set; }
    public int SkillCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public SkillCategory? SkillCategory { get; set; }
}

public class EducationItem
{
    public int Id { get; set; }
    public string Degree { get; set; } = string.Empty;
    public string Institution { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string DateRange { get; set; } = string.Empty;
    public string Result { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}

public class Certification
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}

public class CoreSubject
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}

public class ContactMessage
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedUtc { get; set; }
    public bool IsRead { get; set; }
}
