using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using AquariumAutomationAPI.Repository;
using AquariumAutomationAPI.Services;
using AquariumAutomationAPI.DTO;
using AquariumAutomationAPI.Models;

namespace AquariumAutomationAPI.Controllers
{
    public class AccountController(IDataRepository _dataRepository, ITokenService _tokenService) : BaseApiController
    {
        [HttpPost]
        [Route("register")]
        public ActionResult<UserDTO> Register(RegisterDTO registerDTO)
        {
            User? existing_user = _dataRepository.GetUserByEmail(registerDTO.UserEmail);
            if (existing_user != null)
            {
                return BadRequest("User already exists in database.");
            }

            bool validity = CheckRequestValidity(registerDTO);
            if (!validity)
            {
                return BadRequest("Check your inputs");
            }
            using var hmac = new HMACSHA512();
            var _passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password));
            var _passwordSalt = hmac.Key;

            User registerUser = new User
            {
                UserId = 0,
                AccountId = 0,
                UserFirstName = registerDTO.UserFirstName,
                UserLastName = registerDTO.UserLastName,
                UserEmail = registerDTO.UserEmail.ToLower(),
                PhoneNumber = registerDTO.UserPhoneNumber,
                UserTypeId = 2,
                UserCreatedOnDate = DateTime.UtcNow,
                PasswordHash = _passwordHash,
                PasswordSalt = _passwordSalt,
                AccountCreatedDate = DateTime.UtcNow
            };
            registerUser = _dataRepository.RegisterUserToDb(registerUser);
            if (registerUser.UserId != -1 && registerUser.AccountId != -1)
            {
                return Ok
                (
                    new UserDTO
                    {
                        Email = registerUser.UserEmail,
                        Token = _tokenService.CreateToken(registerUser)
                    }
                );
            }
            return StatusCode(500);
        }
        private bool CheckRequestValidity(RegisterDTO registerDTO)
        {
            if (!CheckStringValidity(registerDTO.UserFirstName) || !CheckStringValidity(registerDTO.UserEmail) || !CheckStringValidity(registerDTO.Password))
            {
                return false;
            }
            return true;
        }
        private bool CheckStringValidity(string checkString)
        {
            if (checkString.Length < 3)
                return false;
            Regex regex = new Regex("[a-zA-Z]");
            return regex.IsMatch(checkString);
        }


        [HttpPost]
        [Route("login")]
        public ActionResult<UserDTO> Login(LoginDTO loginDTO)
        {
            User? user = _dataRepository.GetUserByEmail(loginDTO.Email);
            if (user == null)
            {
                return Unauthorized("Invalid username.");
            }
            if (user.PasswordHash == null)
            {
                return Unauthorized("Invalid password");
            }
            if (user.PasswordSalt == null)
            {
                return StatusCode(500);
            }
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Invalid password");
                }
            }
            return Ok(
                new UserDTO
                {
                    Email = user.UserEmail,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }
    }
}
