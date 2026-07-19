namespace CourseManagement.Infrastructure.Configurations;
public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        //chỉ định tên bảng trong cơ sở dữ liệu
        builder.ToTable("Enrollments");
        //khóa chính
        builder.HasKey(e => e.Id);
        //các thuộc tính và ràng buộc dữ liệu
        builder.Property(e => e.EnrolledAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        //quan hệ
        builder.HasOne(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseId)
            .OnDelete(DeleteBehavior.Cascade);//xóa dây chuyền khi xóa khóa học
        builder.HasOne(e => e.Student)
            .WithMany(u => u.Enrollments)
            .HasForeignKey(e => e.StudentId)
            .OnDelete(DeleteBehavior.Cascade);//xóa dây chuyền khi xóa sinh viên
    }
}