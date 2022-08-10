using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.JWTClass;
using WebAPI.Models;
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
        private readonly IConfiguration _configuration;
        private JwtSettings _jwtSettings;
        public AccountController(JwtSettings jwtSettings, IConfiguration configuration)
        {
            this._configuration = configuration;
            this._jwtSettings = jwtSettings;
        }
        [HttpPost]
        public IActionResult GetToken([FromBody] UserLogins userLogins)
        {
            try
            {
                using (var db = new WebApiDBContext(this._configuration))
                {
                    var Token = new UserTokens();
                    //TODO: compare password base on hash, not plain text
                    var Valid = db.User.Any(x => x.UserName.Equals(userLogins.UserName) && x.Password.Equals(userLogins.Password));
                    if (Valid)
                    {
                        var user = db.User.FirstOrDefault(x => x.UserName.Equals(userLogins.UserName));
                        Token = JwtHelpers.GenTokenkey(new UserTokens()
                        {
                            EmailId = user.EmailAddress,
                            GuidId = Guid.NewGuid(),
                            UserName = user.UserName,
                            Id = user.Id,
                        }, _jwtSettings);
                    }
                    else
                    {
                        return BadRequest($"wrong user name or password");
                    }
                    return Ok(Token);
                }       
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

    }
}
