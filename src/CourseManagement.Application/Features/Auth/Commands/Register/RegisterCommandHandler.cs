namespace CourseManagement.Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork; // <-- 1. Thêm UnitOfWork để lưu DB
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterCommandHandler(
        IUserRepository userRepository, 
        IUnitOfWork unitOfWork,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // 1. Kiểm tra email tồn tại
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
        {
            // Nên dùng Custom Exception thay vì Exception thường
            throw new InvalidOperationException("Email đã được sử dụng trong hệ thống."); 
        }

        // 2. Tạo User entity
        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Address = request.Address
        };

        // 3. Thêm vào Repository & Lưu thực sự xuống Database
        await _userRepository.AddUserAsync(newUser);
        await _unitOfWork.SaveChangesAsync(cancellationToken); // <-- 2. QUAN TRỌNG: Phải SaveChanges!

        // 4. Tạo JWT Token
        var token = _jwtTokenGenerator.GenerateToken(newUser);

        // 5. Trả về Response DTO
        return new AuthResponseDto(
            newUser.Id,
            newUser.Username,
            newUser.Email,
            newUser.Address,
            token
        );
    }
}