using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrewsAPI.Model;

namespace ScrewsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller 
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegUser regUser)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(regUser.Password);

            if (await screwsContext.Instance.Users.FirstOrDefaultAsync(u => u.Email == regUser.Email) != null)
                return BadRequest("Этот email уже занят");

            User user = new User(regUser.Name, regUser.Email, passwordHash);

            screwsContext.Instance.Users.Add(user);
            await screwsContext.Instance.SaveChangesAsync();

            var token = screwsContext.Instance.GenerateToken(user);
            return Ok(new { TOKEN = token });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUser loginUser)
        {
            var user = await screwsContext.Instance.Users.FirstOrDefaultAsync(u => u.Email == loginUser.Email);

            if(user == null)
                return NotFound("Такого пользователя не существует");

            if (!BCrypt.Net.BCrypt.Verify(loginUser.Password, user.PasswordHash))
                return BadRequest("Неверный пароль");

            var token = screwsContext.Instance.GenerateToken(user);
            return Ok(new { TOKEN = token });
        }
    }
}
