namespace CourseManagement.Application.Features.Courses.Commands.DeleteCourse;
public class DeleteCourseValidator : AbstractValidator<DeleteCourseCommand>
{
    public DeleteCourseValidator()
    {
        RuleFor(x => x.CourseId).NotEmpty().WithMessage("CourseId không được để trống.");
    }
}