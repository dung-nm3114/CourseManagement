namespace CourseManagement.Application.Features.Courses.Commands.UpdateCourse;
public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, CourseDto>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    public UpdateCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }
    public async Task<CourseDto> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(request.CourseId);
        if (course == null)
        {
            throw new KeyNotFoundException($"Không tìm thấy khóa học với Id: {request.CourseId}");
        }
        if (course.InstructorId != _currentUserService.UserId)
        {
            throw new UnauthorizedAccessException("Bạn không có quyền cập nhật khóa học này."); 
        }
        course.Title = request.Title;
        course.Description = request.Description;
        course.Price = request.Price;
        course.Thumbnail = request.Thumbnail;
        await _courseRepository.UpdateAsync(course);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new CourseDto(
            course.Id,
            course.Title,
            course.Description,
            course.Price,
            course.Thumbnail,
            course.CreatedAt,
            course.InstructorId,
            course.Instructor.Username
        );
    }
}