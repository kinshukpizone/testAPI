using Presentation.DataTransferObject;
using Presentation.ResponseSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.IServices
{
    public interface IStateService
    {
        Task<IdenticalServiceResponse<bool>> CreateAsync(StateModel model);
        Task<IdenticalServiceResponse<bool>> UpdateAsync(Guid id, StateModel model);
        Task<IdenticalServiceResponse<bool>> DeleteAsync(Guid id);
        Task<IdenticalServiceResponse<PaginationResponse<StateModel>>> GetAllAsync(RequestPaginationModel pagination);
        Task<IdenticalServiceResponse<StateModel?>> GetByIdAsync(Guid id);
    }
}
