namespace CourseManagement.Application.Features.Courses.Commands.DeleteCourse;
public record DeleteCourseCommand(
    Guid CourseId
) : IRequest<bool>;