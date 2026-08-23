using MediatR;
using Microsoft.AspNetCore.Identity;
using SushiMarket.BLL.MediatR.Auth.Login;
using SushiMarket.DAL.Entities.Users;

namespace SushiMarket.BLL.MediatR.Auth.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Unit>
    {
        private readonly UserManager<User> _userManager;

        public LoginCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Model;

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            return Unit.Value;
        }
    }
}