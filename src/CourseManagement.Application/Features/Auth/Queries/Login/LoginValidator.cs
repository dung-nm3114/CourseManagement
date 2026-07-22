namespace CourseManagement.Application.Features.Auth.Queries.Login;
// sử dụng FluentValidation để validate dữ liệu đầu vào của LoginQuery
public class LoginValidator : AbstractValidator<LoginQuery>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email là bắt buộc.")
            .EmailAddress().WithMessage("Định dạng email không hợp lệ.")
            .MaximumLength(100).WithMessage("Email không được vượt quá 100 ký tự.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password là bắt buộc.")
            .MinimumLength(6).WithMessage("Password phải có ít nhất 6 ký tự.");
    }
}