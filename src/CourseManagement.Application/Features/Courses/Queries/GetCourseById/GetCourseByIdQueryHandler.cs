namespace CourseManagement.Application.Features.Courses.Queries.GetCourseById;
public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, CourseDto>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUserRepository _userRepository;

    public GetCourseByIdQueryHandler(ICourseRepository courseRepository, IUserRepository userRepository)
    {
        _courseRepository = courseRepository;
        _userRepository = userRepository;
    }

    public async Task<CourseDto> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(request.Id);

        if (course == null)
        {
            throw new KeyNotFoundException($"Course with Id {request.Id} not found.");
        }

        var instructor = await _userRepository.GetByIdAsync(course.InstructorId);

        return new CourseDto(
            course.Id,
            course.Title,
            course.Description,
            course.Price,
            course.Thumbnail,
            course.CreatedAt,
            course.InstructorId,
            instructor.Username
        );
    }
}