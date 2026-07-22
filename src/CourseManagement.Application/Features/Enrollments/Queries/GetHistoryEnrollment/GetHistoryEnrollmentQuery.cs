using CourseManagement.Application.Common.Models;
namespace CourseManagement.Application.Features.Enrollments.Queries.GetHistoryEnrollment;
public record GetHistoryEnrollmentQuery(
    string ? keyword = null,
    decimal ? minPrice = null,
    decimal ? maxPrice = null,
    int pageNumber = 1,
    int pageSize = 10) : IRequest<PagedResult<EnrollmentDto>>;
