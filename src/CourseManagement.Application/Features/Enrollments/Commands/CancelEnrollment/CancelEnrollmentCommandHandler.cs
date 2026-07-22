using CourseManagement.Application.Features.Enrollments.Commands.CancelEnrollment;
using CourseManagement.Application.Features.Enrollments;
namespace CourseManagement.Application.Features.Enrollments.Commands.CancelEnrollment;
public class CancelEnrollmentCommandHandler : IRequestHandler<CancelEnrollmentCommand, bool>
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly ICurrentUserService _currentUserService;

    private readonly IUnitOfWork _unitOfWork;

    public CancelEnrollmentCommandHandler(IEnrollmentRepository enrollmentRepository, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
    {
        _enrollmentRepository = enrollmentRepository;
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(CancelEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(request.EnrollmentId);
        if (enrollment == null)
        {
            throw new KeyNotFoundException($"Không tìm thấy enrollment với Id: {request.EnrollmentId}");
        }
        if (enrollment.StudentId != _currentUserService.UserId)
        {
            throw new UnauthorizedAccessException("Bạn không có quyền hủy enrollment này.");    
        }
        await _enrollmentRepository.DeleteAsync(enrollment.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}