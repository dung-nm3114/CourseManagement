# src/CourseManagement.Api/Dockerfile

# Stage 1: Dùng SDK để build code
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy các file .csproj vào trước để tận dụng cache NuGet của Docker
COPY ["src/CourseManagement.Domain/CourseManagement.Domain.csproj", "src/CourseManagement.Domain/"]
COPY ["src/CourseManagement.Application/CourseManagement.Application.csproj", "src/CourseManagement.Application/"]
COPY ["src/CourseManagement.Infrastructure/CourseManagement.Infrastructure.csproj", "src/CourseManagement.Infrastructure/"]
COPY ["src/CourseManagement.Api/CourseManagement.Api.csproj", "src/CourseManagement.Api/"]

# Khôi phục các thư viện (Restore NuGet packages)
RUN dotnet restore "src/CourseManagement.Api/CourseManagement.Api.csproj"

# Copy toàn bộ mã nguồn còn lại vào và build dự án
COPY . .
WORKDIR "/app/src/CourseManagement.Api"
RUN dotnet publish "CourseManagement.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Sử dụng Runtime nhỏ gọn để chạy ứng dụng
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Mở cổng 8080 (Mặc định của .NET 8 trong Docker)
EXPOSE 8080

ENTRYPOINT ["dotnet", "CourseManagement.Api.dll"]