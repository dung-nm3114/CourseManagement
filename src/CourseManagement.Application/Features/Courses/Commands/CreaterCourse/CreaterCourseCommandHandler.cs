namespace CourseManagement.Application.Features.Courses.Commands.CreaterCourse;
public class CreaterCourseCommandHandler : IRequestHandler<CreaterCourseCommand, CourseDto>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public CreaterCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IUserRepository userRepository)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
        _userRepository = userRepository;
    }
    public async Task<CourseDto> Handle(CreaterCourseCommand request, CancellationToken cancellationToken)
    {
        var course = new Course
        {
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            Thumbnail = request.Thumbnail,
            InstructorId = _currentUserService.UserId,
            CreatedAt = DateTime.UtcNow
        };

        await _courseRepository.AddAsync(course);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var instructor = await _userRepository
                .GetByIdAsync(course.InstructorId);

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