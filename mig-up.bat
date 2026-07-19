@echo off
dotnet ef database update %1 --project src/CourseManagement.Infrastructure --startup-project src/CourseManagement.WebApi