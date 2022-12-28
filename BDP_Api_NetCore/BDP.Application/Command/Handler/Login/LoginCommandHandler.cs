using BDP.Application.Command.Request.Login;
using BDP.Application.Enum;
using BDP.Application.Helpers;
using BDP.Application.ViewModel;
using BDP.Domain.Entities.User;
using BDP.Domain.Enum;
using BDP.Infrastructure.Data.Interface;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace BDP.Application.Command.Handler.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, ResponseDTOViewModel<JwtSecurityToken>>
    {
        private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;

        public LoginCommandHandler(IUserRepository userRepository, IConfiguration configuration)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public async Task<ResponseDTOViewModel<JwtSecurityToken>> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                request = request ?? throw new ArgumentNullException(nameof(request));
                var user = await userRepository.GetUserByEmail(request.login.EmailAddress, cancellationToken);
                if (user == null)
                {
                    return new ResponseDTOViewModel<JwtSecurityToken>()
                    {
                        Status = Status.Error,
                        Message = ErrorResource.EmailAddress_Invalid_NotMatch
                    };
                }
                var passowrd = request.login.Password;
                var salt = user.PasswordSalt;
                var passwordHash = user.PasswordHash;

                var isPasswordValid = Encryption.ValidatePassword(passowrd, passwordHash, salt);
                if (!isPasswordValid)
                {
                    return new ResponseDTOViewModel<JwtSecurityToken>()
                    {
                        Status = Status.Error,
                        Message = ErrorResource.Password_NotMatch_Email
                    };
                }

                var token = GetToken(user);
                return new ResponseDTOViewModel<JwtSecurityToken>()
                {
                    Status = Status.Success,
                    Message = "Login Success",
                    Value = token
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorResource.LoginHandlerCommand, ex);
            }
        }

        private JwtSecurityToken GetToken(UserEntity user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Email, user.EmailAddress),
                new Claim(ClaimTypes.Role, user.RoleId.ToString()),
            };
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            return new JwtSecurityToken(
               expires: DateTime.Now.AddHours(5),
               claims: claims,
               signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
               );
        }
    }
}
