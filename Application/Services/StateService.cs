using Application.General.IPersistence;
using Application.Services.IServices;
using AutoMapper;
using Domain.Entities.Location;
using Domain.General.Enums;
using Microsoft.EntityFrameworkCore;
using Presentation.DataTransferObject;
using Presentation.ResponseSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class StateService : IStateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IdenticalServiceResponse<bool>> CreateAsync(StateModel model)
        {
            try
            {
                IdenticalServiceResponse<bool> response = new IdenticalServiceResponse<bool>();
                var IsExist = await _unitOfWork.StateRepository.Any(x => x.StateName.ToLower().Equals(model.StateName) && x.ActivityStatus!.ToLower() != ActivityStatus.SOFT_DELETE.ToString().ToLower());
                if (!IsExist)
                {
                    var entity = _mapper.Map<State>(model);
                    await _unitOfWork.StateRepository.AddAsync(entity);
                    await _unitOfWork.SaveAsync();
                    response.Result = true;
                    return response;
                }
                response.Errors = $"{model.StateName} already exist";
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(message: ex.Message, innerException: ex.InnerException);
            }
        }

        public async Task<IdenticalServiceResponse<bool>> DeleteAsync(Guid id)
        {
            try
            {
                IdenticalServiceResponse<bool> response = new IdenticalServiceResponse<bool>();
                var IsExist = await _unitOfWork.StateRepository.Any(x => x.Id == id && x.ActivityStatus!.ToLower() == ActivityStatus.SOFT_DELETE.ToString().ToLower());
                if (!IsExist)
                {
                    response.Result = await _unitOfWork.StateRepository.SoftDeleteAsync(id);
                    return response;
                }
                response.Errors = ResponseMessage.DataNotFound.ToString();
                response.Result = false;
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(message: ex.Message, innerException: ex.InnerException);
            };
        }

        public async Task<IdenticalServiceResponse<PaginationResponse<StateModel>>> GetAllAsync(RequestPaginationModel pagination)
        {
            try
            {
                IdenticalServiceResponse<PaginationResponse<StateModel>> response = new() { Result = new PaginationResponse<StateModel>() };

                var Entities = _unitOfWork.StateRepository
                    .GetAll(predicate: x => x.ActivityStatus!.ToLower() != ActivityStatus.SOFT_DELETE.ToString().ToLower(), include: a => a.AsNoTracking().Include(i => i.Cities))
                   .Skip((pagination.PageNumber - 1) * pagination.Size)
                   .Take(pagination.Size).ToList();

                var totalData = await _unitOfWork.StateRepository.CountAsync(x => x.ActivityStatus!.ToLower() != ActivityStatus.SOFT_DELETE.ToString().ToLower());

                response.Result.CurrentPageSize = pagination.PageNumber;
                response.Result.CurrentPageSize = pagination.Size;
                response.Result.TotalData = totalData;
                response.Result.TotaPages = totalData / pagination.Size + ((totalData % pagination.Size) == 0 ? 0 : 1);
                response.Result.ResultList = _mapper.Map<List<StateModel>>(Entities);

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(message: ex.Message, innerException: ex.InnerException);
            };
        }

        public async Task<IdenticalServiceResponse<StateModel?>> GetByIdAsync(Guid id)
        {
            try
            {
                IdenticalServiceResponse<StateModel?> response = new IdenticalServiceResponse<StateModel?>();
                var IsEntity = await _unitOfWork.StateRepository.Get(predicate: x => x.Id == id && x.ActivityStatus!.ToLower() != ActivityStatus.SOFT_DELETE.ToString().ToLower()).FirstOrDefaultAsync();

                if (IsEntity != null)
                {
                    response.Result = _mapper.Map<StateModel>(IsEntity);
                    return response;
                }
                response.Errors = ResponseMessage.DataNotFound.ToString();
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(message: ex.Message, innerException: ex.InnerException);
            };
        }

        public async Task<IdenticalServiceResponse<bool>> UpdateAsync(Guid id, StateModel model)
        {
            try
            {
                IdenticalServiceResponse<bool> response = new IdenticalServiceResponse<bool>();
                var IsExist = await _unitOfWork.StateRepository.Any(x => x.Id.Equals(id) && x.ActivityStatus!.ToLower() != ActivityStatus.SOFT_DELETE.ToString().ToLower());
                if (IsExist)
                {
                    var entity = _mapper.Map<State>(model);
                    await _unitOfWork.StateRepository.UpdateAsync(entity);
                    response.Result = true;
                    return response;
                }
                response.Errors = ResponseMessage.DataNotFound.ToString();
                response.Result = false;
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(message: ex.Message, innerException: ex.InnerException);
            }
        }
    }
}
