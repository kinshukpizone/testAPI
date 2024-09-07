using Presentation.DataTransferObject.Admin;
using Presentation.ResponseSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.IServices.Admin
{
    public interface IPermissionService
    {
        Task<IdenticalServiceResponse<bool>> CreateAsync(List<PermissionModel> model);
        Task<IdenticalServiceResponse<bool>> UpdateAsync(Guid id, PermissionModel model);
        Task<IdenticalServiceResponse<bool>> DeleteAsync(Guid id);
        Task<IdenticalServiceResponse<PaginationResponse<PermissionModel>>> GetAllAsync(RequestPaginationModel pagination);
        Task<IdenticalServiceResponse<PermissionModel?>> GetByIdAsync(Guid id);
    }
}
