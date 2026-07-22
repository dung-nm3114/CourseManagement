namespace CourseManagement.Infrastructure.Repositories;
public class EnrollmentRepository : IEnrollmentRepository
{       
    private readonly ApplicationDbContext _context;

    public EnrollmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Enrollment?> GetByIdAsync(Guid id)
    {
        return await _context.Enrollments
            .Include(e => e.Course)
            .Include(e => e.Student)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Enrollment?> GetByStudentIdAsync(Guid StudentId)
    {
        return await _context.Enrollments
            .Include(e => e.Course)
            .Include(e => e.Student)
            .FirstOrDefaultAsync(e => e.StudentId == StudentId);
    }

    public async Task AddAsync(Enrollment enrollment)
    {
        await _context.Enrollments.AddAsync(enrollment);
        
    }

    public async Task DeleteAsync(Guid id)
    {
        var enrollment = await _context.Enrollments.FindAsync(id);
        if (enrollment != null)
        {
            _context.Enrollments.Remove(enrollment);
        }
        else
        {
            throw new KeyNotFoundException($"Khong tìm thấy enrollment với Id: {id}");
        }
    }

    public async Task<(List<Enrollment> Items, int TotalCount)> GetEnrollmentPagedAsync(
        Guid studentId,
        string? keyword, 
        decimal? minPrice, 
        decimal? maxPrice, 
        int pageNumber, 
        int pageSize)
    {
        var query = _context.Enrollments.Where(e => e.StudentId == studentId).AsNoTracking().AsQueryable();
        // Filter by keyword (if provided)
        if (!string.IsNullOrEmpty(keyword))
        {
            var cleanKeyord = keyword.Trim().ToLower();
            query = query.Where(e => e.Course.Title.ToLower().Contains(cleanKeyord) || e.Course.Description.ToLower().Contains(cleanKeyord));
        }

        // Filter by price range (if provided)
        if (minPrice.HasValue)
        {
            query = query.Where(e => e.Course.Price >= minPrice.Value);
        }
        if (maxPrice.HasValue)
        {
            query = query.Where(e => e.Course.Price <= maxPrice.Value);
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(e => e.EnrolledAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(e => e.Course)
            .Include(e => e.Student)
            .ToListAsync();

        return (items, totalCount);
    }
}