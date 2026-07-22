namespace CourseManagement.Application.Features.Enrollments;
public record EnrollmentDto(
    Guid Id,
    Guid CourseId,
    string CourseTitle,
    decimal CoursePrice,
    DateTime EnrolledAt,
    Guid StudentId,
    string StudentName
);