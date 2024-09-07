using Application.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Presentation.DataTransferObject;
using Presentation.ResponseSchema;
using webapi._Base;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : BasedController
    {
        private readonly ICityService _CityService;

        public CityController(ICityService CityService)
        {
            _CityService = CityService ?? throw new ArgumentNullException(nameof(CityService));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route(CREATE_ROUTE)]
        public async Task<IActionResult> CreateAsync(CityModel model)
        {
            CustomJsonResponse<bool> customJsonResponse = new CustomJsonResponse<bool>();
            if (ModelState.IsValid)
            {
                var result = await _CityService.CreateAsync(model);
                return customJsonResponse.CREATE(result);
            }
            return BadRequest();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete, Route(DELETE_ROUTE)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            CustomJsonResponse<bool> customJsonResponse = new CustomJsonResponse<bool>();
            if (ModelState.IsValid)
            {
                var result = await _CityService.DeleteAsync(id);
                return customJsonResponse.DELETE(result);
            }
            return BadRequest();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut, Route(UPDATE_ROUTE)]
        public async Task<IActionResult> UpdateAsync(Guid id, CityModel model)
        {
            CustomJsonResponse<bool> customJsonResponse = new CustomJsonResponse<bool>();
            if (ModelState.IsValid)
            {
                var result = await _CityService.UpdateAsync(id, model);
                return customJsonResponse.UPDATE(result);
            }
            return BadRequest();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet, Route(GET_ALL_ROUTE)]
        public async Task<IActionResult> GetAllAsync(int pageNumber = 1, int size = 10)
        {
            CustomJsonResponse<PaginationResponse<CityModel>> customJsonResponse = new();
            if (ModelState.IsValid)
            {
                RequestPaginationModel pagination = new RequestPaginationModel() { PageNumber = pageNumber, Size = size };
                var result = await _CityService.GetAllAsync(pagination);
                return customJsonResponse.GET(result);
            }
            return BadRequest();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route(GET_BY_ID_ROUTE)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            CustomJsonResponse<CityModel?> customJsonResponse = new CustomJsonResponse<CityModel?>();
            var result = await _CityService.GetByIdAsync(id);
            return customJsonResponse.GET(result);
        }
    }
}
