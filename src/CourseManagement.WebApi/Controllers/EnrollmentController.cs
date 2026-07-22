
using CourseManagement.Application.Features.Enrollments.Commands.EnrollCourse;
using CourseManagement.Application.Features.Enrollments.Commands.CancelEnrollment;
using CourseManagement.Application.Features.Enrollments.Queries.GetHistoryEnrollment;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CourseManagement.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class EnrollmentController : ControllerBase
{
    private readonly IMediator _mediator;
    public EnrollmentController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("enroll")]
    [Authorize(Roles="Student,Instructor,Admin")] // Yêu cầu người dùng phải đăng nhập để đăng ký khóa học
    public async Task<IActionResult> EnrollCourse([FromBody] EnrollCourseCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }   
    [HttpDelete("cancel/{enrollmentId}")]
    [Authorize(Roles="Student,Instructor,Admin")] // Yêu cầu người dùng phải đăng nhập để hủy đăng ký khóa học
    public async Task<IActionResult> CancelEnrollment(Guid enrollmentId)
    {
        var command = new CancelEnrollmentCommand(enrollmentId);
        var result = await _mediator.Send(command);
        if (result)
        {
            return Ok(new { message = "Hủy đăng ký khóa học thành công." });
        }
        else
        {
            return NotFound(new { message = "Không tìm thấy đăng ký khóa học." });
        }
    }
    [HttpGet("get-paged")]
    [Authorize(Roles="Student,Instructor,Admin")] // Yêu cầu người dùng phải đăng nhập để xem lịch sử đăng ký khóa học
    public async Task<IActionResult> GetCoursesPaged(
        [FromQuery] string? keyword = null,
        [FromQuery] decimal? minPrice = null,
        [FromQuery] decimal? maxPrice = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetHistoryEnrollmentQuery(keyword, minPrice, maxPrice, pageNumber, pageSize);
        var result = await _mediator.Send(query);
        return Ok(result);  
    }
    
}