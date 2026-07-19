namespace CourseManagement.Application.Features.Auth.Commands.Register;
// định nghĩa kết quả trả về khi Auth thành công, dùng chung cho cả login và Register
public record AuthResponseDto(
    Guid Id,
    string Username,
    string Email,
    string Address,
    string token
);
//command nhận thông tin đăng ký từ client
public record RegisterCommand(
    string Username,
    string Email,
    string Password,
    string Address
) : IRequest<AuthResponseDto>;