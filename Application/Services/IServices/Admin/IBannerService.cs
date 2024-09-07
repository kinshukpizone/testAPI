using Presentation.DataTransferObject;
using Presentation.DataTransferObject.Admin;
using Presentation.ResponseSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.IServices.Admin
{
    public interface IBannerService
    {
        Task<IdenticalServiceResponse<bool>> CreateAsync(BannerModel model);
        Task<IdenticalServiceResponse<bool>> UpdateAsync(Guid id, BannerModel model);
        Task<IdenticalServiceResponse<bool>> DeleteAsync(Guid id);
        Task<IdenticalServiceResponse<PaginationResponse<BannerModel>>> GetAllAsync(RequestPaginationModel pagination);
        Task<IdenticalServiceResponse<BannerModel?>> GetByIdAsync(Guid id);
    }
}
