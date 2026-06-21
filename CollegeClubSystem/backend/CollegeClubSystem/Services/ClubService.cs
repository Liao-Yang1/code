using Microsoft.EntityFrameworkCore;

public class ClubService : IClubService
{
    private readonly AppDbContext _context;
    
    public ClubService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Club> CreateClub(CreateClubRequest request, int userId)
    {
        var club = new Club
        {
            Name = request.Name,
            Description = request.Description,
            Category = request.Category,
            FounderId = userId,
            Status = 0
        };
        
        _context.Clubs.Add(club);
        await _context.SaveChangesAsync();
        
        var member = new Member
        {
            UserId = userId,
            ClubId = club.Id,
            Role = 1
        };
        _context.Members.Add(member);
        await _context.SaveChangesAsync();
        
        return club;
    }
    
    public async Task<List<ClubResponse>> GetAllClubs()
    {
        return await _context.Clubs
            .Include(c => c.Founder)
            .Select(c => new ClubResponse
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Category = c.Category,
                LogoUrl = c.LogoUrl,
                FounderName = c.Founder.RealName,
                Status = c.Status,
                CreatedAt = c.CreatedAt
            })
            .ToListAsync();
    }
    
    public async Task<ClubResponse> GetClubById(int id)
    {
        return await _context.Clubs
            .Include(c => c.Founder)
            .Where(c => c.Id == id)
            .Select(c => new ClubResponse
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Category = c.Category,
                LogoUrl = c.LogoUrl,
                FounderName = c.Founder.RealName,
                Status = c.Status,
                CreatedAt = c.CreatedAt
            })
            .FirstOrDefaultAsync();
    }
    
    public async Task<Club> UpdateClub(int id, CreateClubRequest request)
    {
        var club = await _context.Clubs.FindAsync(id);
        if (club == null) throw new Exception("社团不存在");
        
        club.Name = request.Name;
        club.Description = request.Description;
        club.Category = request.Category;
        club.UpdatedAt = DateTime.Now;
        
        await _context.SaveChangesAsync();
        return club;
    }
    
    public async Task<bool> DeleteClub(int id)
    {
        var club = await _context.Clubs.FindAsync(id);
        if (club == null) return false;
        
        _context.Clubs.Remove(club);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> JoinClub(int clubId, int userId)
    {
        if (await _context.Members.AnyAsync(m => m.UserId == userId && m.ClubId == clubId))
            throw new Exception("已加入该社团");
        
        var member = new Member
        {
            UserId = userId,
            ClubId = clubId,
            Role = 0
        };
        
        _context.Members.Add(member);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> ApproveClub(int clubId)
    {
        var club = await _context.Clubs.FindAsync(clubId);
        if (club == null) return false;
        
        club.Status = 1;
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<List<ClubResponse>> GetMyClubs(int userId)
    {
        var memberShips = await _context.Members.Where(m => m.UserId == userId).Select(m => m.ClubId).ToListAsync();
        
        return await _context.Clubs
            .Include(c => c.Founder)
            .Where(c => memberShips.Contains(c.Id))
            .Select(c => new ClubResponse
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Category = c.Category,
                LogoUrl = c.LogoUrl,
                FounderName = c.Founder.RealName,
                Status = c.Status,
                CreatedAt = c.CreatedAt
            })
            .ToListAsync();
    }
}