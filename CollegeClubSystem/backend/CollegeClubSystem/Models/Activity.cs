using System.ComponentModel.DataAnnotations;

public class Activity
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }
    
    [MaxLength(500)]
    public string Description { get; set; }
    
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
    [MaxLength(200)]
    public string Location { get; set; }
    
    public int MaxParticipants { get; set; }
    
    public int ClubId { get; set; }
    public Club Club { get; set; }
    
    public int Status { get; set; } = 0;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public ICollection<ActivityParticipant> Participants { get; set; } = new List<ActivityParticipant>();
}