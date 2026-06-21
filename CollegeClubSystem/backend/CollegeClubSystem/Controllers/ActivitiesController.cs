using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/activities")]
public class ActivitiesController : ControllerBase
{
    private readonly IActivityService _activityService;
    
    public ActivitiesController(IActivityService activityService)
    {
        _activityService = activityService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllActivities()
    {
        var activities = await _activityService.GetAllActivities();
        return Ok(activities);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetActivityById(int id)
    {
        var activity = await _activityService.GetActivityById(id);
        if (activity == null) return NotFound();
        return Ok(activity);
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateActivity([FromBody] CreateActivityRequest request)
    {
        try
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);
            var activity = await _activityService.CreateActivity(request, userId);
            return CreatedAtAction(nameof(GetActivityById), new { id = activity.Id }, activity);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpPost("{id}/apply")]
    [Authorize]
    public async Task<IActionResult> ApplyActivity(int id)
    {
        try
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);
            await _activityService.ApplyActivity(id, userId);
            return Ok(new { message = "报名成功" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpPost("{id}/approve")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> ApproveActivity(int id)
    {
        var success = await _activityService.ApproveActivity(id);
        if (!success) return NotFound();
        return Ok(new { message = "审核通过" });
    }
}