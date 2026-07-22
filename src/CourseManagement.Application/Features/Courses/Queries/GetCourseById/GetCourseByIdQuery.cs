namespace CourseManagement.Application.Features.Courses.Queries.GetCourseById;
public record GetCourseByIdQuery(Guid Id) : IRequest<CourseDto>;