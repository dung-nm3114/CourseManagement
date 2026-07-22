namespace CourseManagement.Application.Features.Auth;

public record AuthResponseDto(
    Guid Id,
    string Username,
    string Email,
    string Address,
    string Token
);