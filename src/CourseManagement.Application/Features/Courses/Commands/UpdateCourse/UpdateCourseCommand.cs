namespace CourseManagement.Application.Features.Courses.Commands.UpdateCourse;
public record UpdateCourseCommand(
    Guid CourseId,
    string Title,
    string Description,
    decimal Price,
    string? Thumbnail
) : IRequest<CourseDto>;