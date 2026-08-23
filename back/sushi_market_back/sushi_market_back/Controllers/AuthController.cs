using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SushiMarket.BLL.DTOs.Auth;
using SushiMarket.BLL.MediatR.Auth.Login;
using SushiMarket.BLL.MediatR.Auth.Register;
using SushiMarket.DAL.Entities.Users;

namespace SushiMarket.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AuthController(
            IMediator mediator,
            SignInManager<User> signInManager,
            UserManager<User> userManager)
        {
            _mediator = mediator;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                await _mediator.Send(new RegisterCommand(dto));

                var user = await _userManager.FindByEmailAsync(dto.Email);

                await _signInManager.SignInAsync(user!, isPersistent: true);
                var roles = await _userManager.GetRolesAsync(user!);

                return Ok(new
                {
                    message = "Registration and auto-login successful",
                    user = new
                    {
                        email = user!.Email,
                        name = user.Name,
                        surname = user.Surname,
                        roles = roles
                    }
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                await _mediator.Send(new LoginCommand(dto));

                var user = await _userManager.FindByEmailAsync(dto.Email);
                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid email or password." });
                }

                await _signInManager.SignInAsync(user, isPersistent: true);
                var roles = await _userManager.GetRolesAsync(user);

                return Ok(new
                {
                    message = "Login successful",
                    user = new
                    {
                        email = user.Email,
                        name = user.Name,
                        surname = user.Surname,
                        roles = roles
                    }
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Logout successful" });
        }

        [HttpGet("user-info")]
        [Authorize]
        public IActionResult GetUserInfo()
        {
            return Ok(new
            {
                Email = User.Identity?.Name,
                IsAuthenticated = User.Identity?.IsAuthenticated,
                Roles = User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role).Select(c => c.Value)
            });
        }
    }
}