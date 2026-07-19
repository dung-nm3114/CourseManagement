namespace CourseManagement.Application.Interfaces;

public interface IUserRepository
{
    // Lấy thông tin User bằng Email (dùng khi Đăng nhập hoặc kiểm tra trùng lặp khi Đăng ký)
    Task<User?> GetByEmailAsync(string email);

    // Thêm một User mới vào hệ thống
    Task AddUserAsync(User user);
}