using Microsoft.EntityFrameworkCore;

public class ActivityService : IActivityService
{
    private readonly AppDbContext _context;
    
    public ActivityService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Activity> CreateActivity(CreateActivityRequest request, int userId)
    {
        var activity = new Activity
        {
            Title = request.Title,
            Description = request.Description,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Location = request.Location,
            MaxParticipants = request.MaxParticipants,
            ClubId = request.ClubId,
            Status = 0
        };
        
        _context.Activities.Add(activity);
        await _context.SaveChangesAsync();
        return activity;
    }
    
    public async Task<List<Activity>> GetAllActivities()
    {
        return await _context.Activities.Include(a => a.Club).ToListAsync();
    }
    
    public async Task<Activity> GetActivityById(int id)
    {
        return await _context.Activities.Include(a => a.Club).FirstOrDefaultAsync(a => a.Id == id);
    }
    
    public async Task<bool> ApplyActivity(int activityId, int userId)
    {
        var activity = await _context.Activities.FindAsync(activityId);
        if (activity == null) throw new Exception("活动不存在");
        
        if (await _context.ActivityParticipants.AnyAsync(ap => ap.ActivityId == activityId && ap.UserId == userId))
            throw new Exception("已报名该活动");
        
        var participant = new ActivityParticipant
        {
            ActivityId = activityId,
            UserId = userId,
            Status = 0
        };
        
        _context.ActivityParticipants.Add(participant);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> ApproveActivity(int activityId)
    {
        var activity = await _context.Activities.FindAsync(activityId);
        if (activity == null) return false;
        
        activity.Status = 1;
        await _context.SaveChangesAsync();
        return true;
    }
}