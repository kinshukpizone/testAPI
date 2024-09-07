using Presentation.DataTransferObject.Admin;
using Presentation.ResponseSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.IServices.Admin
{
    public interface ICategoryService
    {
        Task<IdenticalServiceResponse<bool>> CreateAsync(CategoryModel model);
        Task<IdenticalServiceResponse<bool>> UpdateAsync(Guid id, CategoryModel model);
        Task<IdenticalServiceResponse<bool>> DeleteAsync(Guid id);
        Task<IdenticalServiceResponse<PaginationResponse<CategoryModel>>> GetAllAsync(RequestPaginationModel pagination);
        Task<IdenticalServiceResponse<CategoryModel?>> GetByIdAsync(Guid id);
    }
}
