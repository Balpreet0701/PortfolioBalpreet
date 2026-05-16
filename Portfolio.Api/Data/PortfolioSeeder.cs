using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Models;

namespace Portfolio.Api.Data;

public static class PortfolioSeeder
{
    public static async Task SeedAsync(PortfolioDbContext db)
    {
        if (await db.Profiles.AnyAsync())
        {
            return;
        }

        db.Profiles.Add(new Profile
        {
            Name = "Balpreet Kaur",
            Title = "Full-Stack .NET Developer",
            Email = "balpreetkaur.it24@gmail.com",
            Phone = "+91-9506360328",
            Location = "Kanpur, Uttar Pradesh",
            LinkedInUrl = "https://www.linkedin.com/in/balpreet-kaur-337776200/",
            GitHubUrl = "https://github.com/Balpreet0701",
            Summary = "Software Developer at Persistent Systems with hands-on experience building scalable full-stack applications using ASP.NET Core, C#, Entity Framework, SQL Server, HTML, CSS, JavaScript, and Bootstrap. Experienced in RESTful APIs, healthcare workflows, production support, and responsive user interfaces."
        });

        db.Experiences.AddRange(
            new Experience
            {
                Role = "Software Developer",
                Company = "Persistent Systems",
                Location = "Pune, India",
                StartDate = "Nov 2024",
                EndDate = "Present",
                SortOrder = 1,
                Highlights =
                {
                    Highlight("Developed and maintained scalable full-stack applications using ASP.NET Core, C#, and SQL Server.", 1),
                    Highlight("Designed and implemented RESTful APIs following clean architecture principles.", 2),
                    Highlight("Built responsive and user-friendly UI components using HTML, CSS, JavaScript, and Bootstrap.", 3),
                    Highlight("Contributed to a US healthcare domain project with care management, utilization management, and appeals workflows.", 4),
                    Highlight("Collaborated with cross-functional teams in an agile environment to deliver features on time.", 5),
                    Highlight("Resolved production issues, fixed bugs, and improved application stability.", 6)
                }
            },
            new Experience
            {
                Role = "Intern Software Developer",
                Company = "Persistent Systems",
                Location = "Remote",
                StartDate = "Jan 2024",
                EndDate = "Oct 2024",
                SortOrder = 2,
                Highlights =
                {
                    Highlight("Gained hands-on experience with .NET Framework and ASP.NET Web API development.", 1),
                    Highlight("Worked with C#, Entity Framework, and SQL Server for backend development.", 2),
                    Highlight("Applied object-oriented programming concepts in real-world scenarios.", 3)
                }
            });

        db.Projects.AddRange(
            new Project
            {
                Name = "US Healthcare Web Application",
                DateRange = "April 2024 - Present",
                Summary = "Healthcare platform work focused on care management, utilization management, appeals, grievances, REST APIs, and SQL-backed backend operations.",
                SortOrder = 1,
                Technologies =
                {
                    Technology(".NET Framework", 1),
                    Technology("ASP.NET Web API", 2),
                    Technology("C#", 3),
                    Technology("SQL Server", 4),
                    Technology("HTML", 5),
                    Technology("CSS", 6),
                    Technology("JavaScript", 7)
                },
                Highlights =
                {
                    ProjectHighlight("Developing and enhancing features for a US healthcare based web application.", 1),
                    ProjectHighlight("Working on core modules including Care Management, Utilization Management, and Appeals & Grievances.", 2),
                    ProjectHighlight("Designing and consuming RESTful APIs using ASP.NET Web API.", 3),
                    ProjectHighlight("Implementing backend logic and database operations using Entity Framework and SQL Server.", 4),
                    ProjectHighlight("Improving application performance and fixing bugs in existing features.", 5)
                }
            },
            new Project
            {
                Name = "Waste Food Management System",
                DateRange = "Nov 2024 - Jan 2025",
                Summary = "A web application connecting food donors, receivers, NGOs, and waste management organizations to reduce food wastage.",
                SortOrder = 2,
                Technologies =
                {
                    Technology(".NET", 1),
                    Technology("ASP.NET MVC", 2),
                    Technology("ASP.NET Web API", 3),
                    Technology("SQL Server", 4),
                    Technology("JWT Authentication", 5)
                },
                Highlights =
                {
                    ProjectHighlight("Built a web application to connect food donors and receivers to reduce food wastage.", 1),
                    ProjectHighlight("Implemented user authentication and authorization using JWT.", 2),
                    ProjectHighlight("Developed donor features to post food quantity and status.", 3),
                    ProjectHighlight("Enabled NGOs and waste management organizations to connect with donors.", 4),
                    ProjectHighlight("Designed the database and handled backend operations using SQL Server.", 5)
                }
            },
            new Project
            {
                Name = "India-Dekho",
                DateRange = "Smart India Hackathon",
                Summary = "A website showcasing India's cultural heritage and diversity with a responsive user experience.",
                SortOrder = 3,
                Technologies =
                {
                    Technology("HTML", 1),
                    Technology("CSS", 2),
                    Technology("JavaScript", 3),
                    Technology("SQL Server", 4)
                },
                Highlights =
                {
                    ProjectHighlight("Developed a website showcasing the cultural heritage and diversity of India.", 1),
                    ProjectHighlight("Designed responsive UI for a better user experience across devices.", 2),
                    ProjectHighlight("Collaborated in a team during Smart India Hackathon.", 3)
                }
            });

        db.SkillCategories.AddRange(
            SkillCategory("Languages", 1, "C#", "C++", "JavaScript"),
            SkillCategory("Backend", 2, ".NET Core", "ASP.NET Web API", "ASP.NET MVC", "Entity Framework"),
            SkillCategory("Frontend", 3, "HTML", "CSS", "Bootstrap", "React"),
            SkillCategory("Database", 4, "SQL Server"),
            SkillCategory("Cloud", 5, "Microsoft Azure Basics"),
            SkillCategory("Tools", 6, "Git", "GitHub", "Postman", "Visual Studio"));

        db.Education.AddRange(
            new EducationItem
            {
                Degree = "B.Tech. (RTU)",
                Institution = "Jaipur Engineering College and Research Centre",
                Location = "Jaipur, Rajasthan",
                DateRange = "2020 - 2024",
                Result = "9.94 CGPA",
                SortOrder = 1
            },
            new EducationItem
            {
                Degree = "Higher Secondary",
                Institution = "Mariampur Sr. Sec. School",
                Location = "Kanpur, Uttar Pradesh",
                DateRange = "2019",
                Result = "84.2%",
                SortOrder = 2
            },
            new EducationItem
            {
                Degree = "Secondary",
                Institution = "Mariampur Sr. Sec. School",
                Location = "Kanpur, Uttar Pradesh",
                DateRange = "2017",
                Result = "10 CGPA",
                SortOrder = 3
            });

        db.Certifications.AddRange(
            Certification("Microsoft Azure Fundamentals (AZ-900) - Microsoft", 1),
            Certification("Microsoft Azure AI Fundamentals (AI-900) - Microsoft", 2),
            Certification("AWS Certified AI Practitioner - Amazon Web Services", 3),
            Certification("Google Cloud Generative AI Leader - Google Cloud", 4));

        db.CoreSubjects.AddRange(
            CoreSubject("Object-Oriented Programming", 1),
            CoreSubject("Data Structures & Algorithms", 2),
            CoreSubject("Problem Solving", 3),
            CoreSubject("Operating Systems", 4),
            CoreSubject("Computer Networks", 5));

        await db.SaveChangesAsync();
    }

    private static ExperienceHighlight Highlight(string text, int sortOrder) => new()
    {
        Text = text,
        SortOrder = sortOrder
    };

    private static ProjectTechnology Technology(string name, int sortOrder) => new()
    {
        Name = name,
        SortOrder = sortOrder
    };

    private static ProjectHighlight ProjectHighlight(string text, int sortOrder) => new()
    {
        Text = text,
        SortOrder = sortOrder
    };

    private static SkillCategory SkillCategory(string name, int sortOrder, params string[] skills)
    {
        var category = new SkillCategory
        {
            Name = name,
            SortOrder = sortOrder
        };

        for (var index = 0; index < skills.Length; index++)
        {
            category.Skills.Add(new Skill
            {
                Name = skills[index],
                SortOrder = index + 1
            });
        }

        return category;
    }

    private static Certification Certification(string name, int sortOrder) => new()
    {
        Name = name,
        SortOrder = sortOrder
    };

    private static CoreSubject CoreSubject(string name, int sortOrder) => new()
    {
        Name = name,
        SortOrder = sortOrder
    };
}
