namespace CourseManagement.Application.Interfaces;
public interface IEnrollmentRepository
{
    Task<Enrollment?> GetByIdAsync(Guid id);
    Task<Enrollment?> GetByStudentIdAsync(Guid StudentId);
    Task AddAsync(Enrollment enrollment);
    Task DeleteAsync(Guid id);
    Task<(List<Enrollment> Items, int TotalCount)> GetEnrollmentPagedAsync(
        Guid studentId,
        string? keyword, 
        decimal? minPrice, 
        decimal? maxPrice, 
        int pageNumber, 
        int pageSize);
}   