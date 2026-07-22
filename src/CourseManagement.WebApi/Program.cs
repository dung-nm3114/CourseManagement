using System.Text;
using System.Text.Json; // 👈 Thêm namespace này để serialize JSON response
using CourseManagement.Application;
using CourseManagement.Application.Interfaces;
using CourseManagement.Infrastructure;
using CourseManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// 1. ĐĂNG KÝ CONTROLLERS
// ============================================================

builder.Services.AddControllers();


// ============================================================
// 2. ĐĂNG KÝ DATABASE
// ============================================================

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection")
        )
    )
);


// ============================================================
// 3. ĐĂNG KÝ SERVICES CỦA APPLICATION
// ============================================================

// Đăng ký MediatR, FluentValidation,...
builder.Services.AddApplicationServices();


// ============================================================
// 4. ĐĂNG KÝ SERVICES CỦA INFRASTRUCTURE
// ============================================================

// Đăng ký Repository, UnitOfWork, JwtTokenGenerator, CurrentUserService,...
builder.Services.AddInfrastructureServices();


// ============================================================
// 5. CẤU HÌNH JWT AUTHENTICATION
// ============================================================

var jwtSettings = builder.Configuration.GetSection("JwtSettings");

var secretKey = jwtSettings["Secret"]
    ?? throw new InvalidOperationException("JWT Secret is missing.");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Kiểm tra chữ ký của JWT
            ValidateIssuerSigningKey = true,

            // Secret Key dùng để kiểm tra JWT
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secretKey)
            ),

            // Kiểm tra Issuer
            ValidateIssuer = true,
            ValidIssuer = jwtSettings["Issuer"],

            // Kiểm tra Audience
            ValidateAudience = true,
            ValidAudience = jwtSettings["Audience"],

            // Kiểm tra thời hạn của Token
            ValidateLifetime = true,

            // Không cho phép sai lệch thời gian
            ClockSkew = TimeSpan.Zero
        };

        // 👈 BỔ SUNG: Xử lý custom message trả về khi dính lỗi Auth (401 & 403)
        options.Events = new JwtBearerEvents
        {
            // Xử lý khi User KHÔNG ĐỦ QUYỀN (Role không khớp -> Lỗi 403 Forbidden)
            OnForbidden = async context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    statusCode = 403,
                    message = "Bạn không có quyền thực hiện chức năng này (Chỉ dành cho Giảng viên hoặc Admin)."
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            },

            // Xử lý khi User CHƯA ĐĂNG NHẬP hoặc Token không hợp lệ/hết hạn (Lỗi 401 Unauthorized)
            OnChallenge = async context =>
            {
                // Bỏ qua xử lý mặc định của ASP.NET Core để không bị đè response
                context.HandleResponse();

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    statusCode = 401,
                    message = "Bạn chưa đăng nhập hoặc phiên làm việc đã hết hạn. Vui lòng đăng nhập lại."
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        };
    });


// ============================================================
// 6. ĐĂNG KÝ AUTHORIZATION
// ============================================================

builder.Services.AddAuthorization();


// ============================================================
// 7. CẤU HÌNH SWAGGER
// ============================================================

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "CourseManagement API",
            Version = "v1"
        }
    );

    // Định nghĩa Bearer Authentication
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,

            Description =
                "Nhập JWT Token vào đây. " +
                "Không cần nhập chữ 'Bearer ' ở đầu."
        }
    );

    // Cho phép Swagger sử dụng JWT Token
    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        }
    );
});


// ============================================================
// BUILD APPLICATION
// ============================================================

var app = builder.Build();


// ============================================================
// 8. HTTP REQUEST PIPELINE
// ============================================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Chuyển HTTP sang HTTPS
app.UseHttpsRedirection();


// ============================================================
// 9. AUTHENTICATION & AUTHORIZATION
// ============================================================

// Authentication phải chạy trước Authorization
app.UseAuthentication();

app.UseAuthorization();


// ============================================================
// 10. MAP CONTROLLERS
// ============================================================

app.MapControllers();


// ============================================================
// 11. CHẠY APPLICATION
// ============================================================

app.Run();