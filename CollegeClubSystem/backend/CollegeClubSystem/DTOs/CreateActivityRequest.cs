public class CreateActivityRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Location { get; set; }
    public int MaxParticipants { get; set; }
    public int ClubId { get; set; }
}