using CourseManagement.Application.Features.Courses.Commands.CreaterCourse;
using CourseManagement.Application.Features.Courses.Commands.DeleteCourse;
using CourseManagement.Application.Features.Courses.Commands.UpdateCourse;
using CourseManagement.Application.Features.Courses.Queries.GetCourseById;
using CourseManagement.Application.Features.Courses.Queries.GetCoursePaged;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CourseManagement.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly IMediator _mediator;
    public CourseController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("create")]
    [Authorize(Roles="Instructor,Admin")] // Yêu cầu người dùng phải đăng nhập để tạo khóa học
    public async Task<IActionResult> CreateCourse([FromBody] CreaterCourseCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpPut("update")]
    [Authorize(Roles="Instructor,Admin")] // Yêu cầu người dùng phải đăng nhập để cập nhật khóa học
    public async Task<IActionResult> UpdateCourse([FromBody] UpdateCourseCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpDelete("delete/{courseId}")]
    [Authorize(Roles="Instructor,Admin")] // Yêu cầu người dùng phải đăng nhập để xóa khóa học
    public async Task<IActionResult> DeleteCourse(Guid courseId)
    {
        var command = new DeleteCourseCommand(courseId);
        var result = await _mediator.Send(command);
        if (result)
        {
            return Ok(new { message = "Xóa khóa học thành công." });
        }
        else
        {
            return NotFound(new { message = "Không tìm thấy khóa học." });
        }
    }
    [HttpGet("get/{courseId}")]
    public async Task<IActionResult> GetCourseById(Guid courseId)
    {
        var query = new GetCourseByIdQuery(courseId);
        var result = await _mediator.Send(query);
        if (result != null)
        {
            return Ok(result);
        }
        else
        {
            return NotFound(new { message = "Không tìm thấy khóa học." });
        }
    }   
    [HttpGet("get-paged")]
    public async Task<IActionResult> GetCoursesPaged(
        [FromQuery] string? keyword = null,
        [FromQuery] decimal? minPrice = null,
        [FromQuery] decimal? maxPrice = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetCoursesPagedQuery(keyword, minPrice, maxPrice, pageNumber, pageSize);
        var result = await _mediator.Send(query);
        return Ok(result);  
    }
}