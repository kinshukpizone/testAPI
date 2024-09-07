using Presentation.DataTransferObject.Admin;
using Presentation.ResponseSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.IServices.Admin
{
    public interface IProductService
    {
        Task<IdenticalServiceResponse<bool>> CreateAsync(ProductModel model);
        Task<IdenticalServiceResponse<bool>> UpdateAsync(Guid id, ProductModel model);
        Task<IdenticalServiceResponse<bool>> DeleteAsync(Guid id);
        Task<IdenticalServiceResponse<PaginationResponse<ProductModel>>> GetAllAsync(RequestPaginationModel pagination);
        Task<IdenticalServiceResponse<ProductModel?>> GetByIdAsync(Guid id);
    }
}
