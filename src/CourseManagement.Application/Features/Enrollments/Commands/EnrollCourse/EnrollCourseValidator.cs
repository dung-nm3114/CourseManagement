namespace CourseManagement.Application.Features.Enrollments.Commands.EnrollCourse;
public class EnrollCourseValidator : AbstractValidator<EnrollCourseCommand>
{
    public EnrollCourseValidator()
    {
        RuleFor(x => x.CourseId).NotEmpty().WithMessage("CourseId is required.");
    }
}