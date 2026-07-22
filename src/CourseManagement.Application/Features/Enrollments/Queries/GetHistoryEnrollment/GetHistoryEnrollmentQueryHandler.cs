using CourseManagement.Application.Common.Models;
namespace CourseManagement.Application.Features.Enrollments.Queries.GetHistoryEnrollment;
public class GetHistoryEnrollmentQueryHandler : IRequestHandler<GetHistoryEnrollmentQuery, PagedResult<EnrollmentDto>>
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetHistoryEnrollmentQueryHandler(IEnrollmentRepository enrollmentRepository, ICurrentUserService currentUserService)
    {
        _enrollmentRepository = enrollmentRepository;
        _currentUserService = currentUserService;
    }

    public async Task<PagedResult<EnrollmentDto>> Handle(GetHistoryEnrollmentQuery request, CancellationToken cancellationToken)
    {
        
        var (items, totalCount) = await _enrollmentRepository.GetEnrollmentPagedAsync(

            _currentUserService.UserId,
            request.keyword,
            request.minPrice,
            request.maxPrice,
            request.pageNumber,
            request.pageSize
        );
        //map trực tiếp danh sách Enrollment sang EnrollmentDto (Đã có Course nhờ .Include() ở Repo)
        var enrollmentDtos = items.Select(enrollment => new EnrollmentDto(
            enrollment.Id,  
            enrollment.CourseId,
            enrollment.Course?.Title ?? string.Empty,
            enrollment.Course?.Price ?? 0,
            enrollment.EnrolledAt,
            enrollment.StudentId,
            enrollment.Student?.Username ?? string.Empty
        )).ToList();
        return new PagedResult<EnrollmentDto>(
            enrollmentDtos,
            request.pageNumber,
            request.pageSize,
            totalCount
        );
    }

    
}