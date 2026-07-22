namespace CourseManagement.Application.Features.Enrollments.Commands.CancelEnrollment;
public record CancelEnrollmentCommand(Guid EnrollmentId) : IRequest<bool>;