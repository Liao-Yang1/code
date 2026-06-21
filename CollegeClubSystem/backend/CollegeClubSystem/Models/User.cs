using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Username { get; set; }
    
    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string PasswordHash { get; set; }
    
    [MaxLength(50)]
    public string RealName { get; set; }
    
    [MaxLength(20)]
    public string StudentId { get; set; }
    
    [MaxLength(50)]
    public string Department { get; set; }
    
    [MaxLength(20)]
    public string Phone { get; set; }
    
    public int RoleId { get; set; }
    public Role Role { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}