namespace CourseManagement.Application.Features.Auth.Commands.Register;
// sử dụng FluentValidation để validate dữ liệu đầu vào của RegisterCommand
public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Tên tài khoản không được để trống.")
            .MaximumLength(50).WithMessage("Tên tài khoản không được vượt quá 50 ký tự.");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email là bắt buộc.")
            .EmailAddress().WithMessage("Định dạng email không hợp lệ.")
            .MaximumLength(100).WithMessage("Email không được vượt quá 100 ký tự.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password là bắt buộc.")
            .MinimumLength(6).WithMessage("Password phải có ít nhất 6 ký tự.");
        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address là bắt buộc.")
            .MaximumLength(200).WithMessage("Address không được vượt quá 200 ký tự.");
    }
}