using Presentation.DataTransferObject;
using Presentation.ResponseSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.IServices
{
    public interface ICityService
    {
        Task<IdenticalServiceResponse<bool>> CreateAsync(CityModel model);
        Task<IdenticalServiceResponse<bool>> UpdateAsync(Guid id, CityModel model);
        Task<IdenticalServiceResponse<bool>> DeleteAsync(Guid id);
        Task<IdenticalServiceResponse<PaginationResponse<CityModel>>> GetAllAsync(RequestPaginationModel pagination);
        Task<IdenticalServiceResponse<CityModel?>> GetByIdAsync(Guid id);
    }
}
