namespace CourseManagement.Application.Interfaces;
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);// Lưu các thay đổi vào cơ sở dữ liệu và trả về số lượng bản ghi bị ảnh hưởng
}
