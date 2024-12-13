using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrewsAPI.Model;

namespace ScrewsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        [HttpGet("GetAllUsers")]
        [Authorize(Policy = "ManagerRole")]
        public async Task<IActionResult> GetAllUsers()
        {
            List<User> users = await screwsContext.Instance.Users.ToListAsync();
            return Ok(users);
        }
        [HttpGet("GetUser")]
        [Authorize(Policy = "UserRole")]
        public async Task<IActionResult> GetUser(int id)
        {
            if(await screwsContext.Instance.Users.FirstOrDefaultAsync(u => u.Id == id) is User user)
                return Ok(user);
            else
                return NotFound("Пользователь не найден");
        }
        [HttpPut("UpdateUser")]
        [Authorize(Policy = "UserRole")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUser newUser)
        {
            if (await screwsContext.Instance.Users.FirstOrDefaultAsync(u => u.Id == id) is User currentUser)
            {
                if(currentUser.Email != newUser.Email)
                {
                    if (await screwsContext.Instance.Users.FirstOrDefaultAsync(u => u.Email == newUser.Email) != null)
                        return BadRequest("Этот email уже занят");
                }
                currentUser.Name = newUser.Name;
                currentUser.Email = newUser.Email;
                if(newUser.Password != string.Empty)
                    currentUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
                currentUser.Role = newUser.Role;

                screwsContext.Instance.Users.Update(currentUser);
                await screwsContext.Instance.SaveChangesAsync();
                return Ok("Успешное редактирование");
            }
            else
                return NotFound("Пользователь не найден");
        }
        [HttpDelete("DeleteUser")]
        [Authorize(Policy = "AdministratorRole")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (await screwsContext.Instance.Users.FirstOrDefaultAsync(u => u.Id == id) is User user)
            {
                screwsContext.Instance.Users.Remove(user);
                await screwsContext.Instance.SaveChangesAsync();
                return Ok("Успешное удаление");
            }
            else
                return NotFound("Пользователь не найден");
        }

    }
}
