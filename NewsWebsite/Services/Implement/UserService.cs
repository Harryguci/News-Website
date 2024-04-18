using Domain.Domain.Common;
using Domain.Helpers;
using Domain.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NewsWebsite.Domain.Dtos;
using NewsWebsite.Domain.Entities;
using NewsWebsite.Domain.Repositories.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Domain.Services.Implement
{
    public class UserService : IUserService
    {

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private IUserRepository _userRepository;

        //private List<Account> _users = new List<Account> {
        //        new Account { Id = "AC$00000", Username = "User", Email = "test", Password = "test", Roles = "user" }};

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, IUserRepository userRepository)
        {
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
        }

        public AuthenticateResponse? Authenticate(AuthenticateRequest model)
        {
            var user = _userRepository.Where(x => x.Username == model.Username).FirstOrDefault();


            // return null if user not found
            if (user == null) return null;

            var isMatch = SecurePasswordHasher.Verify(model.Password, user.Password);

            if (!isMatch) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(new UserDto(user.Username, user.Password, user.Phone, user.Role), token);
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            return (await _userRepository.GetListAsync())
                .Select(p => new UserDto()
                {
                    Username = p.Username,
                    Password = p.Password,
                    Phone = p.Phone,
                    Role = new NewsWebsite.Domain.ValueObjects.RoleObject(p.Role)
                });
        }

        public UserDto? GetById(Guid id)
        {
            return _userRepository
                .Where(x => x.Guid == id)
                .Select(p => new UserDto()
                {
                    Username = p.Username,
                    Password = p.Password,
                    Phone = p.Phone,
                    Role = new NewsWebsite.Domain.ValueObjects.RoleObject(p.Role)
                })
                .FirstOrDefault();
        }

        // helper methods

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("guid", user.Guid.ToString()),
                    new Claim("username", user.Username.ToString()),
                    new Claim("phone", user.Phone ?? ""),
                    new Claim("role", user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
