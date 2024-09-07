using Application.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Presentation.DataTransferObject;
using Presentation.ResponseSchema;
using webapi._Base;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StateController : BasedController
    {
        private readonly IStateService _StateService;

        public StateController(IStateService StateService)
        {
            _StateService = StateService ?? throw new ArgumentNullException(nameof(StateService));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route(CREATE_ROUTE)]
        public async Task<IActionResult> CreateAsync(StateModel model)
        {
            CustomJsonResponse<bool> customJsonResponse = new CustomJsonResponse<bool>();
            if (ModelState.IsValid)
            {
                var result = await _StateService.CreateAsync(model);
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
                var result = await _StateService.DeleteAsync(id);
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
        public async Task<IActionResult> UpdateAsync(Guid id, StateModel model)
        {
            CustomJsonResponse<bool> customJsonResponse = new CustomJsonResponse<bool>();
            if (ModelState.IsValid)
            {
                var result = await _StateService.UpdateAsync(id, model);
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
            CustomJsonResponse<PaginationResponse<StateModel>> customJsonResponse = new();
            if (ModelState.IsValid)
            {
                RequestPaginationModel pagination = new RequestPaginationModel() { PageNumber = pageNumber, Size = size};
                var result = await _StateService.GetAllAsync(pagination);
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
            CustomJsonResponse<StateModel?> customJsonResponse = new CustomJsonResponse<StateModel?>();
            var result = await _StateService.GetByIdAsync(id);
            return customJsonResponse.GET(result);
        }
    }
}
