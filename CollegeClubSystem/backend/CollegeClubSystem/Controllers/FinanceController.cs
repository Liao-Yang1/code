using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/finance")]
public class FinanceController : ControllerBase
{
    private readonly IFinanceService _financeService;
    
    public FinanceController(IFinanceService financeService)
    {
        _financeService = financeService;
    }
    
    [HttpPost("apply")]
    [Authorize]
    public async Task<IActionResult> ApplyFinance([FromBody] FinanceRequest request)
    {
        try
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);
            var record = await _financeService.CreateFinanceRecord(request, userId);
            return Ok(record);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpGet("{clubId}")]
    [Authorize]
    public async Task<IActionResult> GetFinanceRecords(int clubId)
    {
        var records = await _financeService.GetFinanceRecords(clubId);
        return Ok(records);
    }
    
    [HttpPost("{id}/approve")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> ApproveFinance(int id)
    {
        var success = await _financeService.ApproveFinanceRecord(id);
        if (!success) return NotFound();
        return Ok(new { message = "审核通过" });
    }
}