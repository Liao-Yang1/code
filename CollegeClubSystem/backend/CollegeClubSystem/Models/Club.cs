using System.ComponentModel.DataAnnotations;

public class Club
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(500)]
    public string Description { get; set; }
    
    [MaxLength(50)]
    public string Category { get; set; }
    
    [MaxLength(200)]
    public string LogoUrl { get; set; }
    
    public int FounderId { get; set; }
    public User Founder { get; set; }
    
    public int Status { get; set; } = 0;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    public ICollection<Member> Members { get; set; } = new List<Member>();
    public ICollection<Activity> Activities { get; set; } = new List<Activity>();
}