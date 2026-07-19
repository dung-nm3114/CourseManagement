namespace CourseManagement.Infrastructure.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        //chỉ định tên bảng trong cơ sở dữ liệu
        builder.ToTable("Users");
        //khóa chính
        builder.HasKey(u => u.Id);
        //các thuộc tính và ràng buộc dữ liệu
        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(u => u.PasswordHash)
            .IsRequired();
        builder.Property(u => u.Address)
            .IsRequired()
            .HasMaxLength(200);
        
        //lưu enums dưới dạng số nguyên int để tối ưu hiệu năng
        builder.Property(u => u.Role)
            .IsRequired()
            .HasConversion<int>();
        
        builder.Property(u => u.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            
        
    }
}