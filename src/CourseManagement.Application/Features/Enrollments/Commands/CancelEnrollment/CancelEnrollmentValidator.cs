namespace CourseManagement.Application.Features.Enrollments.Commands.CancelEnrollment;
public class CancelEnrollmentValidator : AbstractValidator<CancelEnrollmentCommand>
{
    public CancelEnrollmentValidator()
    {
        RuleFor(x => x.EnrollmentId)
            .NotEmpty().WithMessage("EnrollmentId không được để trống.");
    }
}