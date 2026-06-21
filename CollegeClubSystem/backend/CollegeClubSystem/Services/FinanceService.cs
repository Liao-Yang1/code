using Microsoft.EntityFrameworkCore;

public class FinanceService : IFinanceService
{
    private readonly AppDbContext _context;
    
    public FinanceService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<FinanceRecord> CreateFinanceRecord(FinanceRequest request, int userId)
    {
        var record = new FinanceRecord
        {
            Title = request.Title,
            Description = request.Description,
            Amount = request.Amount,
            Type = request.Type,
            ClubId = request.ClubId,
            CreatedById = userId,
            Status = 0
        };
        
        _context.FinanceRecords.Add(record);
        await _context.SaveChangesAsync();
        return record;
    }
    
    public async Task<List<FinanceRecord>> GetFinanceRecords(int clubId)
    {
        return await _context.FinanceRecords
            .Include(f => f.Club)
            .Include(f => f.CreatedBy)
            .Where(f => f.ClubId == clubId)
            .ToListAsync();
    }
    
    public async Task<bool> ApproveFinanceRecord(int recordId)
    {
        var record = await _context.FinanceRecords.FindAsync(recordId);
        if (record == null) return false;
        
        record.Status = 1;
        await _context.SaveChangesAsync();
        return true;
    }
}