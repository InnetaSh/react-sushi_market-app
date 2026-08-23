using MediatR;
using Microsoft.AspNetCore.Identity;
using SushiMarket.BLL.MediatR.Auth.Register;
using SushiMarket.DAL.Entities.Users;
using SushiMarket.DAL.Enums;

namespace SushiMarket.BLL.MediatR.Auth.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
    {
        private readonly UserManager<User> _userManager;

        public RegisterCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Model;

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists.");
            }

            var user = new User
            {
                UserName = dto.Email,
                Email = dto.Email,
                Name = dto.Name,
                Surname = dto.Surname,
                Role = UserRole.User,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Registration failed: {errors}");
            }

            await _userManager.AddToRoleAsync(user, UserRole.User.ToString());

            return Unit.Value;
        }
    }
}