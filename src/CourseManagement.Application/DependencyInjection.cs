using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using CourseManagement.Application.Common.Behaviors;

namespace CourseManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // 1. Quét và đăng ký tất cả Validators vào DI trước
        services.AddValidatorsFromAssembly(assembly);

        // 2. Đăng ký MediatR
        services.AddMediatR(assembly);

        // 3. Đăng ký Pipeline Behavior cho Validation
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}