using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.JWTClass;
using WebAPI.POCOClass;

namespace WebAPI.Controllers
{
    /// <summary>
    /// API for get jwt token
    ///   <br />
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 8/9/2022 created
    /// </Modified>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// The logins for demo. TODO: get login info from database
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 8/9/2022 created
        /// </Modified>
        private IEnumerable<Users> logins = new List<Users>() {
            new Users() {
                    Id = Guid.NewGuid(),
                        EmailAddress = "adminakp@gmail.com",
                        UserName = "Admin",
                        Password = "Admin",
                },
                new Users() {
                    Id = Guid.NewGuid(),
                        EmailAddress = "adminakp@gmail.com",
                        UserName = "User1",
                        Password = "Admin",
                }
        };
        private readonly JwtSettings jwtSettings;
        public AccountController(JwtSettings jwtSettings)
        {
            this.jwtSettings = jwtSettings;
        }
        [HttpPost]
        public IActionResult GetToken(UserLogins userLogins)
        {
            try
            {
                var Token = new UserTokens();
                var Valid = logins.Any(x => x.UserName.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
                if (Valid)
                {
                    var user = logins.FirstOrDefault(x => x.UserName.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
                    Token = JwtHelpers.GenTokenkey(new UserTokens()
                    {
                        EmailId = user.EmailAddress,
                        GuidId = Guid.NewGuid(),
                        UserName = user.UserName,
                        Id = user.Id,
                    }, jwtSettings);
                }
                else
                {
                    return BadRequest($"wrong password");
                }
                return Ok(Token);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
