using Application.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.DataTransferObject;
using Presentation.ResponseSchema;
using webapi._Base;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BasedController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route(CREATE_ROUTE)]
        public async Task<IActionResult> CreateAsync(RegisterUserModel model)
        {
            CustomJsonResponse<bool> customJsonResponse = new CustomJsonResponse<bool>();
            if (ModelState.IsValid)
            {
                var result = await _accountService.CreateUserAccountAsync(model);
                return customJsonResponse.CREATE(result);
            }
            return BadRequest();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost, Route("sign_in")]
        public async Task<IActionResult> SignInAsync(LoginUserModel model)
        {
            CustomJsonResponse<AuthResponse> customJsonResponse = new CustomJsonResponse<AuthResponse>();
            if (ModelState.IsValid)
            {
                //RunSysProcessUnit(httpContextAccessor.HttpContext!);
                var result = await _accountService.SignInUserAccountAsync(model);
                return customJsonResponse.AUTHENTICATE(result);
            }
            return BadRequest(ModelState);
        }
    }
}
