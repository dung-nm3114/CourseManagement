// Các thư viện hệ thống thông dụng
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading;
global using System.Threading.Tasks;

// Thư viện MediatR và FluentValidation dùng xuyên suốt cho CQRS
global using MediatR;
global using FluentValidation;

// Các Interface và Entity nội bộ của hệ thống (giúp các Feature gọi trực tiếp không cần import lại)
global using CourseManagement.Application.Interfaces;
global using CourseManagement.Domain.Entities;