namespace CourseManagement.Application.Features.Enrollments.Commands.EnrollCourse;
public record EnrollCourseCommand(Guid CourseId) : IRequest<EnrollmentDto>;
