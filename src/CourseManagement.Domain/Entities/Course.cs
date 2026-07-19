
namespace CourseManagement.Domain.Entities;
public class Course
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    private decimal _price;
    public decimal Price
    {
        get => _price;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("khóa học không thể nhỏ hơn 0");
            }
            _price = value;
        }
    }
    public string? Thumbnail { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid InstructorId { get; set; }
    public User Instructor { get; set; } = null!;

    //quan hệ
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    //luật nghiệp vụ domain
    public bool IsFree()
    {
        return Price == 0;
    }
}