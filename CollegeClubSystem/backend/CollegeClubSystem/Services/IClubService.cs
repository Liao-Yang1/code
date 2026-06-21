public interface IClubService
{
    Task<Club> CreateClub(CreateClubRequest request, int userId);
    Task<List<ClubResponse>> GetAllClubs();
    Task<ClubResponse> GetClubById(int id);
    Task<Club> UpdateClub(int id, CreateClubRequest request);
    Task<bool> DeleteClub(int id);
    Task<bool> JoinClub(int clubId, int userId);
    Task<bool> ApproveClub(int clubId);
    Task<List<ClubResponse>> GetMyClubs(int userId);
}