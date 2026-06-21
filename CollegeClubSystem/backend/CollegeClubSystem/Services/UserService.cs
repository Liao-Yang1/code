using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;
    
    public UserService(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }
    
    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var user = await _context.Users.Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Username == request.Username);
        
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new Exception("用户名或密码错误");
        
        var token = GenerateToken(user);
        return new LoginResponse
        {
            Token = token,
            Username = user.Username,
            Role = user.Role.Name,
            UserId = user.Id
        };
    }
    
    public async Task<User> Register(RegisterRequest request)
    {
        if (await _context.Users.AnyAsync(u => u.Username == request.Username))
            throw new Exception("用户名已存在");
        
        if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            throw new Exception("邮箱已被注册");
        
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Student") 
            ?? await _context.Roles.FirstOrDefaultAsync(r => r.Id == 4);
        
        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            RealName = request.RealName,
            StudentId = request.StudentId,
            Department = request.Department,
            Phone = request.Phone,
            RoleId = role?.Id ?? 4
        };
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
    
    public async Task<User> GetUserById(int id)
    {
        return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
    }
    
    public async Task<List<User>> GetAllUsers()
    {
        return await _context.Users.Include(u => u.Role).ToListAsync();
    }
    
    private string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role.Name),
            new Claim("UserId", user.Id.ToString())
        };
        
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(24),
            signingCredentials: credentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}