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
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IdenticalServiceResponse<bool>> CreateAsync(CityModel model)
        {
            try
            {
                IdenticalServiceResponse<bool> response = new IdenticalServiceResponse<bool>();
                var IsExist = await _unitOfWork.CityRepository.Any(x => x.CityName.ToLower().Equals(model.CityName) && x.ActivityStatus!.ToLower() != ActivityStatus.SOFT_DELETE.ToString().ToLower());
                if (!IsExist)
                {
                    var entity = _mapper.Map<City>(model);
                    entity.StateId = model.State.Id;
                    entity.StateNavigation = null;
                    await _unitOfWork.CityRepository.AddAsync(entity);
                    await _unitOfWork.SaveAsync();
                    response.Result = true;
                    return response;
                }
                response.Errors = $"{model.CityName} already exist";
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
                var IsExist = await _unitOfWork.CityRepository.Any(x => x.Id == id && x.ActivityStatus!.ToLower() == ActivityStatus.SOFT_DELETE.ToString().ToLower());
                if (!IsExist)
                {
                    response.Result = await _unitOfWork.CityRepository.SoftDeleteAsync(id);
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

        public async Task<IdenticalServiceResponse<PaginationResponse<CityModel>>> GetAllAsync(RequestPaginationModel pagination)
        {
            try
            {
                IdenticalServiceResponse<PaginationResponse<CityModel>> response = new() { Result = new PaginationResponse<CityModel>() };

                var Entities = _unitOfWork.CityRepository
                    .GetAll(predicate: x => x.ActivityStatus!.ToLower() != ActivityStatus.SOFT_DELETE.ToString().ToLower(), include: a => a.AsNoTracking().Include(i => i.StateNavigation!))
                   .Skip((pagination.PageNumber - 1) * pagination.Size)
                   .Take(pagination.Size).ToList();

                var totalData = await _unitOfWork.CityRepository.CountAsync(x => x.ActivityStatus!.ToLower() != ActivityStatus.SOFT_DELETE.ToString().ToLower());

                response.Result.CurrentPageSize = pagination.PageNumber;
                response.Result.CurrentPageSize = pagination.Size;
                response.Result.TotalData = totalData;
                response.Result.TotaPages = totalData / pagination.Size + ((totalData % pagination.Size) == 0 ? 0 : 1);
                response.Result.ResultList = _mapper.Map<List<CityModel>>(Entities);

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(message: ex.Message, innerException: ex.InnerException);
            };
        }

        public async Task<IdenticalServiceResponse<CityModel?>> GetByIdAsync(Guid id)
        {
            try
            {
                IdenticalServiceResponse<CityModel?> response = new IdenticalServiceResponse<CityModel?>();
                var IsEntity = await _unitOfWork.CityRepository.Get(predicate: x => x.Id == id && x.ActivityStatus!.ToLower() != ActivityStatus.SOFT_DELETE.ToString().ToLower()).FirstOrDefaultAsync();

                if (IsEntity != null)
                {
                    response.Result = _mapper.Map<CityModel>(IsEntity);
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

        public async Task<IdenticalServiceResponse<bool>> UpdateAsync(Guid id, CityModel model)
        {
            try
            {
                IdenticalServiceResponse<bool> response = new IdenticalServiceResponse<bool>();
                var IsExist = await _unitOfWork.CityRepository.Any(x => x.Id.Equals(id) && x.ActivityStatus!.ToLower() != ActivityStatus.SOFT_DELETE.ToString().ToLower());
                if (IsExist)
                {
                    var entity = _mapper.Map<City>(model);
                    entity.StateId = model.State.Id;
                    entity.StateNavigation = null;
                    await _unitOfWork.CityRepository.UpdateAsync(entity);
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
