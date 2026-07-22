namespace CourseManagement.Application.Features.Auth.Queries.Login;

//query nhận thông tin đăng nhập từ client
public record LoginQuery(
    string Email,
    string Password
) : IRequest<AuthResponseDto>;