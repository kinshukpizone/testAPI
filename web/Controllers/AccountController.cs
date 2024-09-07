using Application.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Presentation.DataTransferObject;

namespace web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(ILogger<DashboardController> logger, IAccountService accountService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(RegisterUserModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.CreateUserAccountAsync(model);
                if (result.Successed)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginUserModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.SignInUserAccountAsync(model);
                if (result.Successed)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            return View(model);
        }


    }
}
