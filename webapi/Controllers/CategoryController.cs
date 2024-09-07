using Application.Services.IServices.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.DataTransferObject.Admin;
using Presentation.ResponseSchema;
using webapi._Base;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BasedController
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route(CREATE_ROUTE)]
        public async Task<IActionResult> CreateAsync(CategoryModel model)
        {
            CustomJsonResponse<bool> customJsonResponse = new CustomJsonResponse<bool>();
            if (ModelState.IsValid)
            {
                var result = await _service.CreateAsync(model);
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
                var result = await _service.DeleteAsync(id);
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
        public async Task<IActionResult> UpdateAsync(Guid id, CategoryModel model)
        {
            CustomJsonResponse<bool> customJsonResponse = new CustomJsonResponse<bool>();
            if (ModelState.IsValid)
            {
                var result = await _service.UpdateAsync(id, model);
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
            CustomJsonResponse<PaginationResponse<CategoryModel>> customJsonResponse = new();
            if (ModelState.IsValid)
            {
                RequestPaginationModel pagination = new RequestPaginationModel() { PageNumber = pageNumber, Size = size };
                var result = await _service.GetAllAsync(pagination);
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
            CustomJsonResponse<CategoryModel?> customJsonResponse = new CustomJsonResponse<CategoryModel?>();
            var result = await _service.GetByIdAsync(id);
            return customJsonResponse.GET(result);
        }
    }
}
