public class ActivityParticipant
{
    public int Id { get; set; }
    
    public int ActivityId { get; set; }
    public Activity Activity { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
    
    public int Status { get; set; } = 0;
    
    public DateTime RegisteredAt { get; set; } = DateTime.Now;
}