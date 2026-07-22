namespace CourseManagement.Application.Features.Auth.Commands.Register;

//command nhận thông tin đăng ký từ client
public record RegisterCommand(
    string Username,
    string Email,
    string Password,
    string Address
) : IRequest<AuthResponseDto>;