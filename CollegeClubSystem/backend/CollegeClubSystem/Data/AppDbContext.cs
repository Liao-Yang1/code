using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<ActivityParticipant> ActivityParticipants { get; set; }
    public DbSet<FinanceRecord> FinanceRecords { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
        
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        
        modelBuilder.Entity<Member>()
            .HasIndex(m => new { m.UserId, m.ClubId })
            .IsUnique();
        
        modelBuilder.Entity<ActivityParticipant>()
            .HasIndex(ap => new { ap.ActivityId, ap.UserId })
            .IsUnique();
    }
}