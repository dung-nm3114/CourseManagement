using CourseManagement.Application.Common.Models;
using CourseManagement.Application.Features.Courses.Queries.GetCoursePaged;


namespace CourseManagement.Application.Features.Courses.Queries.GetCoursesPaged;

public class GetCoursesPagedQueryHandler : IRequestHandler<GetCoursesPagedQuery, PagedResult<CourseDto>>
{
    private readonly ICourseRepository _courseRepository;

    public GetCoursesPagedQueryHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<PagedResult<CourseDto>> Handle(
        GetCoursesPagedQuery request, 
        CancellationToken cancellationToken)
    {
        // 1. Hứng trực tiếp Tuple (items, totalCount) từ Repository
        var (items, totalCount) = await _courseRepository.GetPagedAsync(
            request.keyword,
            request.minPrice,
            request.maxPrice,
            request.pageNumber,
            request.pageSize
        );

        // 2. Map trực tiếp danh sách Course sang CourseDto (Đã có Instructor nhờ .Include() ở Repo)
        var courseDtos = items.Select(course => new CourseDto(
            course.Id,
            course.Title,
            course.Description,
            course.Price,
            course.Thumbnail,
            course.CreatedAt,
            course.InstructorId,
            course.Instructor?.Username ?? string.Empty // 👈 Đã có sẵn dữ liệu, không cần query User nữa
        )).ToList();

        // 3. Trả về kết quả phân trang theo đúng thứ tự constructor: (items, pageNumber, pageSize, totalCount)
        return new PagedResult<CourseDto>(
            courseDtos,
            request.pageNumber,
            request.pageSize,
            totalCount
        );
    }
}