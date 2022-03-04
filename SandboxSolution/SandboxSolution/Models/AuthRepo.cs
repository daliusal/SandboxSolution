using SandboxSolution.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Security;

namespace SandboxSolution.Models
{
    public class AuthRepo : IAuthRepo
    {
        //TODO: Implement repository with logi and register methods
        private readonly StoreContext _context;

        public AuthRepo(StoreContext context)
        {
            _context = context;
        }
        public async Task<string> Login(UserDto user, string token)
        {
            //TODO: Check if user data is valid
            User loginUser;
            if(_context.Users.Any(u => u.Username == user.Username))
            {
                loginUser = _context.Users.FirstOrDefault(u => u.Username == user.Username);
                if(VerifyPasswordHash(loginUser, user))
                {
                    return CreateToken(loginUser, token);
                }

                return "Wrong password";
            }

            return "User not found";
        }

        public async Task Register(UserRegistrationDto user)
        {
            //TODO: Check if user data is valid, if so create new user
            if (user.Password == user.RepeatPassword)
            {
                var userToCreate = new User();
                CreatePasswordHash(user.Password, out byte[] hash, out byte[] salt);
                userToCreate.Username = user.Username;
                userToCreate.Email = user.Email;
                userToCreate.PasswordHash = hash;
                userToCreate.PasswordSalt = salt;
                userToCreate.Role = "Customer";
                _context.Users.Add(userToCreate);
                await SaveChanges();
            }
        }
        public void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            //TODO: Generate passowrd hash and salt
            using (var hmac = new HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(User user, UserDto loginInfo)
        {
            //TODO: compare password hashes if equal return true otherwise false
            using(var hmac = new HMACSHA512(user.PasswordSalt)) 
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginInfo.Password));
                return computedHash.SequenceEqual(user.PasswordHash);
            }
        }

        public string CreateToken(User user, string token)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tok = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(tok);

            return jwt;
        }
        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
