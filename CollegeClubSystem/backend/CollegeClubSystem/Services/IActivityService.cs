public interface IActivityService
{
    Task<Activity> CreateActivity(CreateActivityRequest request, int userId);
    Task<List<Activity>> GetAllActivities();
    Task<Activity> GetActivityById(int id);
    Task<bool> ApplyActivity(int activityId, int userId);
    Task<bool> ApproveActivity(int activityId);
}