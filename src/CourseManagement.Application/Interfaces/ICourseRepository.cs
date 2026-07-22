namespace CourseManagement.Application.Interfaces;
public interface ICourseRepository
{
    Task<Course> GetByIdAsync(Guid id);
    Task<IEnumerable<Course>> GetAllAsync();
    Task AddAsync(Course course);
    Task UpdateAsync(Course course);
    Task DeleteAsync(Guid id);

    Task<(List<Course> Items, int TotalCount)> GetPagedAsync(
        string? keyword, 
        decimal? minPrice, 
        decimal? maxPrice, 
        int pageNumber, 
        int pageSize);
        
}