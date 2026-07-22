namespace CourseManagement.Application.Features.Courses.Commands.UpdateCourse;
public class UpdateCourseValidator : AbstractValidator<UpdateCourseCommand>
{
    public UpdateCourseValidator()
    {
        RuleFor(x => x.CourseId).NotEmpty().WithMessage("CourseId không được để trống.");
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title không được để trống.").MaximumLength(100).WithMessage("Title không được vượt quá 100 ký tự.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description không được để trống.").MaximumLength(500).WithMessage("Description không được vượt quá 500 ký tự.");
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Price phải lớn hơn hoặc bằng 0.");
        //kiểm tra thumnail là url hợp lệ
        RuleFor(x => x.Thumbnail)
        .Must(url =>
            string.IsNullOrEmpty(url) ||
            (
                Uri.TryCreate(url, UriKind.Absolute, out var uri)
                && (uri.Scheme == Uri.UriSchemeHttp ||
                    uri.Scheme == Uri.UriSchemeHttps)
            )
        )
        .WithMessage("Thumbnail phải là URL hợp lệ bắt đầu bằng http:// hoặc https://.");
    }
}