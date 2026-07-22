using CourseManagement.Application.Features.Enrollments.Commands.EnrollCourse;
using CourseManagement.Application.Features.Enrollments;

public class EnrollCourseCommandHandler : IRequestHandler<EnrollCourseCommand, EnrollmentDto>
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    private readonly ICurrentUserService _currentUserService;


    public EnrollCourseCommandHandler(
        IEnrollmentRepository enrollmentRepository,
        ICourseRepository courseRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService)
    {
        _enrollmentRepository = enrollmentRepository;
        _courseRepository = courseRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<EnrollmentDto> Handle(EnrollCourseCommand request, CancellationToken cancellationToken)
    {
        // Kiểm tra xem khóa học có tồn tại không
        var course = await _courseRepository.GetByIdAsync(request.CourseId);
        if (course == null)
        {
            throw new KeyNotFoundException($"Không tìm thấy khóa học có id :{request.CourseId}.");
        }

        // Kiểm tra xem sinh viên có tồn tại không
        var student = await _userRepository.GetByIdAsync(_currentUserService.UserId);
        if (student == null)
        {
            throw new KeyNotFoundException($"Bạn cần đăng nhập để thực hiện chức năng này.");
        }
        await _enrollmentRepository.AddAsync(new Enrollment
        {
            CourseId = request.CourseId,
            StudentId = _currentUserService.UserId,
            EnrolledAt = DateTime.UtcNow
        });
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new EnrollmentDto(
            Guid.NewGuid(),
            course.Id,
            course.Title,
            course.Price,
            DateTime.UtcNow,
            student.Id,
            student.Username
        );
    }
}