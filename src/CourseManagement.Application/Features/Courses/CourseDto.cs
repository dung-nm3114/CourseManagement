namespace CourseManagement.Application.Features.Courses;

public record CourseDto(
    Guid Id,
    string Title,
    string Description,
    decimal Price,
    string? Thumbnail,
    DateTime CreatedAt,
    Guid InstructorId,
    string InstructorName
);