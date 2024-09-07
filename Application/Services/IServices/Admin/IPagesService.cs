using Presentation.DataTransferObject.Admin;
using Presentation.ResponseSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.IServices.Admin
{
    public interface IPagesService
    {
        Task<IdenticalServiceResponse<bool>> CreateAsync(PageModel model);
        Task<IdenticalServiceResponse<bool>> UpdateAsync(Guid id, PageModel model);
        Task<IdenticalServiceResponse<bool>> DeleteAsync(Guid id);
        Task<IdenticalServiceResponse<PaginationResponse<PageModel>>> GetAllAsync(RequestPaginationModel pagination);
        Task<IdenticalServiceResponse<PageModel?>> GetByIdAsync(Guid id);
    }
}
