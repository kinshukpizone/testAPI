using Application.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Presentation.DataTransferObject;
using Presentation.ResponseSchema;

namespace web.Controllers
{
    public class StateController : Controller
    {
        private readonly IStateService _stateService;
        private readonly ILogger<StateController> _logger;

        public StateController(IStateService stateService, ILogger<StateController> logger)
        {
            _stateService = stateService ?? throw new ArgumentNullException(nameof(stateService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> Get()
        {
            int pageNumber = 1;
            int size = 10;
            RequestPaginationModel paginationModel = new() { PageNumber = pageNumber, Size = size };
            var result = await _stateService.GetAllAsync(paginationModel);
            if (result.Successed)
            {
                return Json(new { recordsTotal = result.Result!.TotalData, data = result.Result!.ResultList });
            }
            return Json(new { });
        }

        [HttpPost]
        public async Task<JsonResult> Create(string stateName)
        {
            StateModel model = new() { StateName = stateName };
            var result = await _stateService.CreateAsync(model);
            return Json(new { data = result.Result, error = result.Errors, success = result.Successed });
        }
            
        [HttpDelete]
        public async Task<JsonResult> Delete(Guid stateid)
        {
            var result = await _stateService.DeleteAsync(stateid);
            return Json(new { data = result.Result, error = result.Errors, success = result.Successed });
        }

        [HttpPut]
        public async Task<JsonResult> Update(Guid stateid, string stateName)
        {
            StateModel model = new() { Id = stateid,StateName = stateName };
            var result = await _stateService.UpdateAsync(stateid, model);
            return Json(new { data = result.Result, error = result.Errors, success = result.Successed });
        }

        [HttpGet]
        public async Task<JsonResult> GetById(Guid stateid)
        {
            var result = await _stateService.GetByIdAsync(stateid);
            return Json(new { data = result.Result, error = result.Errors, success = result.Successed });
        }

    }
}
