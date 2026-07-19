@echo off
if "%1"=="" (
    echo [Loi] Vui long nhap ten Migration. Vi du: mig-add InitialCreate
    exit /b
)
dotnet ef migrations add %1 --project src/CourseManagement.Infrastructure --startup-project src/CourseManagement.WebApi