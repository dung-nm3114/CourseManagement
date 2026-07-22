
namespace CourseManagement.Application.Features.Courses.Commands.DeleteCourse;
public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, bool>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public DeleteCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }
    public async Task<bool> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(request.CourseId);
        if (course == null)
        {
            throw new KeyNotFoundException($"Không tìm thấy khóa học với Id: {request.CourseId}");
        }
        if (course.InstructorId != _currentUserService.UserId)
        {
            throw new UnauthorizedAccessException("Bạn không có quyền xóa khóa học này.");
        }
        await _courseRepository.DeleteAsync(course.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}