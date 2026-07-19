namespace CourseManagement.Application.Interfaces;
public interface IJwtTokenGenerator
{   
    string GenerateToken(User user);// Phương thức này nhận một đối tượng User và trả về một chuỗi token JWT được tạo ra dựa trên thông tin của người dùng đó.
}