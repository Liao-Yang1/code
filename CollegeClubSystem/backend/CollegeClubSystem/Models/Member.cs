public class Member
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
    
    public int ClubId { get; set; }
    public Club Club { get; set; }
    
    public int Role { get; set; }
    
    public int Status { get; set; } = 0;
    
    public DateTime JoinedAt { get; set; } = DateTime.Now;
}