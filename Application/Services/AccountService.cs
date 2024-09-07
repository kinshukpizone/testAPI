using Application.General.IPersistence;
using Application.Services.IServices;
using AutoMapper;
using Domain.Entities.Account;
using Domain.General.Enums;
using Domain.General.OptionsModel;
using Domain.SeedData;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Presentation.DataTransferObject;
using Presentation.ResponseSchema;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountService> _logger;
        private readonly IMapper _mapper;
        private readonly IOptions<JwtConfigsOptions> _jwtOptions;

        public AccountService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager, ILogger<AccountService> logger, IMapper mapper, IOptions<JwtConfigsOptions> jwtOptions)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _jwtOptions = jwtOptions ?? throw new ArgumentNullException(nameof(jwtOptions));
        }

        private IList<AuthenticationScheme> ExternalLogins { get; set; } = new List<AuthenticationScheme>();

        private enum ClaimsTypeVar
        {
            Id,
            Role,
            Created,
            ExpireTime,
            Name,
            IsAuthenticate,
        };

        public async Task<IdenticalServiceResponse<bool>> CreateUserAccountAsync(RegisterUserModel model)
        {
            var response = new IdenticalServiceResponse<bool>();
            //_unitOfWork.BeginTransaction();
            try
            {
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList<AuthenticationScheme>();
                var user = _mapper.Map<ApplicationUser>(model);
                user.UserName = user.Email;
                var UserExist = await _userManager.FindByEmailAsync(model.Email);
                if (UserExist is not null)
                {
                    response.Errors = $"{model.Email} email already exist!";
                    return response;
                }

                var IdentifyResultForUser = await _userManager.CreateAsync(user, model.Password);
                if (!IdentifyResultForUser.Succeeded)
                {
                    response.Errors = IdentifyResultForUser.Errors.Select(s => s.Description).First();
                    return response;
                }

                var IdentityResultForUserRoles = await _userManager.AddToRoleAsync(user, AppRoles.SUPERADMIN!);
                if (!IdentityResultForUserRoles.Succeeded)
                {
                    await _userManager.DeleteAsync(user);
                    response.Errors = "Client User not created!";
                    return response;
                }

                //_unitOfWork.CommitTransaction();
                response.Errors = null;
                response.Result = true;
                response.Successed = true;
                return response;
            }
            catch (Exception ex)
            {
                //_unitOfWork.RollbackTransaction();
                response.Errors = ex.Message;
                response.Result = false;
                throw;
            }
        }

        public async Task<IdenticalServiceResponse<AuthResponse>> SignInUserAccountAsync(LoginUserModel model)
        {
            var response = new IdenticalServiceResponse<AuthResponse>() { Result = new AuthResponse() };
            try
            {
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList<AuthenticationScheme>();

                var UserExist = await _userManager.Users.Where(x => x.Email == model.Email && x.ActivityStatus!.ToLower() != ActivityStatus.SOFT_DELETE.ToString().ToLower()).FirstOrDefaultAsync();
                if (UserExist is null)
                {
                    response.Errors = "The username you entered isn't connected to an account!";
                    return response;
                }

                var IsPasswordCorrect = await _userManager.CheckPasswordAsync(UserExist, model.Password);
                if (!IsPasswordCorrect)
                {
                    response.Errors = "The password that you've entered is incorrect!";
                    return response;
                }

                var UserRole = await _userManager.GetRolesAsync(UserExist);
                if (UserRole is null)
                {
                    response.Errors = "Invalid Operation, Non Role define!";
                    return response;
                }

                var AccessToken = GenerateJwtToken(UserExist, UserRole.FirstOrDefault()!);
                if (!AccessToken.Successed)
                {
                    response.Errors = AccessToken.Errors;
                    return response;
                }

                response.Result.Id = UserExist.Id;
                response.Result.Token = AccessToken.Result;
                response.Result.Role = UserRole.FirstOrDefault();
                response.Result.LoginTime = DateTime.Now.ToUniversalTime();
                response.Result.IsAuthenticate = true;
                response.Result.UserName= UserExist.FirstName +" " + UserExist.LastName;
                return response;

            }
            catch (Exception ex)
            {
                //_unitOfWork.RollbackTransaction();
                response.Errors += ex.ToString();
                throw;
            }
        }

        public async Task<IdenticalServiceResponse<string>> SignOutUserAccountAsync(Guid id)
        {
            var response = new IdenticalServiceResponse<AuthResponse>() { Result = new AuthResponse() };
            try
            {
            }
            catch (Exception ex)
            {
                //_unitOfWork.RollbackTransaction();
                response.Errors += ex.ToString();
                throw;
            }
            return null;
        }

        // ========================================== PRIVATE METHOD ==================================
        private IdenticalServiceResponse<String> GenerateJwtToken(ApplicationUser user, string role)
        {
            try
            {

                var response = new IdenticalServiceResponse<String>() { Result = "" };
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_jwtOptions.Value.Secret);

                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(type: ClaimsTypeVar.Id.ToString(), value: user.Id.ToString()),
                        new Claim(type: ClaimTypes.Role, value: role),
                        new Claim(type: JwtRegisteredClaimNames.Sub, value: user.Email!.ToString()),
                        new Claim(type: JwtRegisteredClaimNames.Email, value: user.Email!.ToString()),
                        new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
                        new Claim(type: JwtRegisteredClaimNames.Iat, value: DateTime.Now.ToUniversalTime().ToString()),
                        new Claim(type: JwtRegisteredClaimNames.Typ, value: "application/at+jwt"),
                    }),
                    Expires = DateTime.Now.AddDays(1).ToUniversalTime(),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                };

                var token = jwtTokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = jwtTokenHandler.WriteToken(token);

                response.Result = jwtToken;
                return response;

            }
            catch (Exception ex)
            {
                new Exception(ex.Message, ex.InnerException);
                return new IdenticalServiceResponse<string>()
                {
                    Errors = ex.Message
                };
            }
        }

    }
}
