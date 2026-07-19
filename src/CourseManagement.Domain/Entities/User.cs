using CourseManagement.Domain.Enums;
namespace CourseManagement.Domain.Entities;
public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;
    
    public UserRole Role { get; set; } = UserRole.Student;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    //quan hệ 
    public ICollection<Course> Courses { get; set; } = new List<Course>();
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    //luật nghiệp vụ domain
    
    public bool CanCreaterCourse()
    {
        return Role == UserRole.Admin || Role == UserRole.Instructor;
    }
}