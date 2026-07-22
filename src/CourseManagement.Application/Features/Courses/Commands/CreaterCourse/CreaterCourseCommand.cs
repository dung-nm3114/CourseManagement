namespace CourseManagement.Application.Features.Courses.Commands.CreaterCourse;

public record CreaterCourseCommand(
    string Title,
    string Description,
    decimal Price,
    string? Thumbnail
) : IRequest<CourseDto>;