namespace CourseManagement.Infrastructure.Repositories;
public class unitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public unitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}