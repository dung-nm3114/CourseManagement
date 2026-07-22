using CourseManagement.Application.Interfaces;
using CourseManagement.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using CourseManagement.Infrastructure.Authentication;
using CourseManagement.Infrastructure.Services;

namespace CourseManagement.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Đăng ký UserRepository và UnitOfWork để quản lý dữ liệu người dùng
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        // Đăng ký JwtTokenGenerator để tạo token JWT
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        services.AddHttpContextAccessor(); // Đăng ký IHttpContextAccessor để truy cập HttpContext trong CurrentUserService

        return services;
    }
}