using System.ComponentModel.DataAnnotations;

public class FinanceRecord
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }
    
    [MaxLength(500)]
    public string Description { get; set; }
    
    public decimal Amount { get; set; }
    
    public int Type { get; set; }
    
    public int Status { get; set; } = 0;
    
    public int ClubId { get; set; }
    public Club Club { get; set; }
    
    public int CreatedById { get; set; }
    public User CreatedBy { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}