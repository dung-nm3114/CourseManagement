@echo off
dotnet ef migrations script --output migration.sql --project src/CourseManagement.Infrastructure --startup-project src/CourseManagement.WebApi