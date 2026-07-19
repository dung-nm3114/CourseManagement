namespace CourseManagement.Infrastructure.Configurations;
public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        //chỉ định tên bảng trong cơ sở dữ liệu
        builder.ToTable("Courses");
        //khóa chính
        builder.HasKey(c => c.Id);
        //các thuộc tính và ràng buộc dữ liệu
        builder.Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(1000);
        builder.Property(c => c.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        builder.Property(c => c.Thumbnail)
            .HasMaxLength(200);
        builder.Property(c => c.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        //quan hệ
        builder.HasOne(c => c.Instructor)
            .WithMany(u => u.Courses)
            .HasForeignKey(c => c.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);//ngăn chặn xóa dây chuyền tránh mất mát dữ liệu
    }
}