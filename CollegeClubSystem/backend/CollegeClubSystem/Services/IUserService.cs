public interface IUserService
{
    Task<LoginResponse> Login(LoginRequest request);
    Task<User> Register(RegisterRequest request);
    Task<User> GetUserById(int id);
    Task<List<User>> GetAllUsers();
}