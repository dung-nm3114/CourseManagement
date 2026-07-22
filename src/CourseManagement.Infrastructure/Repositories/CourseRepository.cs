namespace CourseManagement.Infrastructure.Repositories;
public class CourseRepository : ICourseRepository
{
    private readonly ApplicationDbContext _context;
    public CourseRepository(ApplicationDbContext context)
    {
        _context = context;
    } 
    public async Task<Course> GetByIdAsync(Guid id)
    {
        return await _context.Courses.Include(c => c.Instructor).FirstOrDefaultAsync(c=> c.Id ==id) ?? throw new KeyNotFoundException($"không tìm thấy khóa học có ID : {id} ");
    }   
    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _context.Courses
            .Include(c => c.Instructor)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task AddAsync(Course course)
    {
        await _context.Courses.AddAsync(course);
    }
    public async Task UpdateAsync(Course course)
    {
        _context.Courses.Update(course);
    }
    public async Task DeleteAsync(Guid id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
        {
            throw new KeyNotFoundException($"không tìm thấy khóa học có ID : {id}");
        }
        _context.Courses.Remove(course);
    }

    public async Task<(List<Course> Items, int TotalCount)> GetPagedAsync(
        string? keyword, 
        decimal? minPrice, 
        decimal? maxPrice, 
        int pageNumber, 
        int pageSize)
    {
        var query = _context.Courses.AsNoTracking().AsQueryable();
        //tìm kiếm gần đúng theo từ khóa title hoặc description
        if (!string.IsNullOrEmpty(keyword))
        {
            var cleanKeyord = keyword.Trim().ToLower();
            query = query.Where(c => c.Title.ToLower().Contains(cleanKeyord) || c.Description.ToLower().Contains(cleanKeyord));

        }
        //lọc theo khoảng giá 
        if (minPrice.HasValue)
        {
            query = query.Where(c => c.Price >= minPrice.Value);
        }
        if (maxPrice.HasValue)
        {
            query = query.Where(c => c.Price <= maxPrice.Value);
        }
        // điếm tổng số bản ghi thỏa điều kiện tìm kiếm
        var totalCount = await query.CountAsync();
        // phân trang
        var items = await query
            .OrderByDescending(c => c.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(c => c.Instructor)
            .ToListAsync();
        return (items, totalCount);
      
    }
}