using Application.General.IPersistence;
using Application.Services.IServices.Admin;
using AutoMapper;
using Domain.Entities.Admin;
using Domain.Entities.Location;
using Domain.General.Enums;
using Presentation.DataTransferObject.Admin;
using Presentation.ResponseSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Admin
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PermissionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IdenticalServiceResponse<bool>> CreateAsync(List<PermissionModel> model)
        {
            IdenticalServiceResponse<bool> response = new IdenticalServiceResponse<bool>();
            //var IsExist = await _unitOfWork.PermissionRepository.Any(x => x.UserID.ToString().Contains() && x.ActivityStatus!.ToLower() != ActivityStatus.SOFT_DELETE.ToString().ToLower());
            if (model != null)
            {
                var entity = _mapper.Map<List<Permission>>(model);               
                await _unitOfWork.PermissionRepository.AddRangeAsync(entity);
                await _unitOfWork.SaveAsync();
                response.Result = true;
                return response;
            }
            response.Errors = $" already exist";
            return response;
        }

        public Task<IdenticalServiceResponse<bool>> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IdenticalServiceResponse<PaginationResponse<PermissionModel>>> GetAllAsync(RequestPaginationModel pagination)
        {
            throw new NotImplementedException();
        }

        public Task<IdenticalServiceResponse<PermissionModel?>> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IdenticalServiceResponse<bool>> UpdateAsync(Guid id, PermissionModel model)
        {
            throw new NotImplementedException();
        }
    }
}
