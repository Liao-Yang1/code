using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/clubs")]
public class ClubsController : ControllerBase
{
    private readonly IClubService _clubService;
    
    public ClubsController(IClubService clubService)
    {
        _clubService = clubService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllClubs()
    {
        var clubs = await _clubService.GetAllClubs();
        return Ok(clubs);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetClubById(int id)
    {
        var club = await _clubService.GetClubById(id);
        if (club == null) return NotFound();
        return Ok(club);
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateClub([FromBody] CreateClubRequest request)
    {
        try
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);
            var club = await _clubService.CreateClub(request, userId);
            return CreatedAtAction(nameof(GetClubById), new { id = club.Id }, club);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateClub(int id, [FromBody] CreateClubRequest request)
    {
        try
        {
            var club = await _clubService.UpdateClub(id, request);
            return Ok(club);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> DeleteClub(int id)
    {
        var success = await _clubService.DeleteClub(id);
        if (!success) return NotFound();
        return Ok();
    }
    
    [HttpPost("{id}/join")]
    [Authorize]
    public async Task<IActionResult> JoinClub(int id)
    {
        try
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);
            await _clubService.JoinClub(id, userId);
            return Ok(new { message = "申请成功" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpPost("{id}/approve")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> ApproveClub(int id)
    {
        var success = await _clubService.ApproveClub(id);
        if (!success) return NotFound();
        return Ok(new { message = "审核通过" });
    }
    
    [HttpGet("my")]
    [Authorize]
    public async Task<IActionResult> GetMyClubs()
    {
        var userId = int.Parse(User.FindFirst("UserId").Value);
        var clubs = await _clubService.GetMyClubs(userId);
        return Ok(clubs);
    }
}