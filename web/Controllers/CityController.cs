using Application.Services;
using Application.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Presentation.DataTransferObject;
using Presentation.ResponseSchema;

namespace web.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        private readonly IStateService _stateService;
        private readonly ILogger<CityController> _logger;

        public CityController(ICityService cityService, IStateService stateService, ILogger<CityController> logger)
        {
            _stateService = stateService ?? throw new ArgumentNullException(nameof(stateService));
            _cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Index()
        {
            int pageNumber = 1;
            int size = 1000;
            RequestPaginationModel paginationModel = new() { PageNumber = pageNumber, Size = size };
            var result = await _stateService.GetAllAsync(paginationModel);
            return View(result.Result!.ResultList);
        }

        public async Task<JsonResult> Get()
        {
            int pageNumber = 1;
            int size = 10;
            RequestPaginationModel paginationModel = new() { PageNumber = pageNumber, Size = size };
            var result = await _cityService.GetAllAsync(paginationModel);
            if (result.Successed)
            {
                return Json(new { recordsTotal = result.Result!.TotalData, data = result.Result!.ResultList });
            }
            return Json(new { });
        }

        [HttpPost]
        public async Task<JsonResult> Create(Guid stateId, string cityname)
        {
            CityModel model = new() { CityName = cityname, State = new() { Id = stateId } };
            var result = await _cityService.CreateAsync(model);
            return Json(new { data = result.Result, error = result.Errors, success = result.Successed });
        }

        [HttpGet]
        public async Task<JsonResult> GetById(Guid cityId)
        {
            var result = await _cityService.GetByIdAsync(cityId);
            return Json(new { data = result.Result, error = result.Errors, success = result.Successed });
        }

        [HttpPut]
        public async Task<JsonResult> Update(Guid stateid, Guid cityid, string cityName)
        {
            CityModel model = new() { Id = cityid, CityName = cityName, State = new() { Id = stateid } };
            var result = await _cityService.UpdateAsync(cityid, model);
            return Json(new { data = result.Result, error = result.Errors, success = result.Successed });
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(Guid cityid)
        {
            var result = await _cityService.DeleteAsync(cityid);
            return Json(new { data = result.Result, error = result.Errors, success = result.Successed });
        }

    }
}
