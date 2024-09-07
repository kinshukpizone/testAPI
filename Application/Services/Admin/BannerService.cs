using Application.General.IPersistence;
using Application.Services.IServices.Admin;
using AutoMapper;
using Domain.Entities.Admin;
using Domain.Entities.Location;
using Domain.General.Enums;
using Microsoft.EntityFrameworkCore;
using Presentation.DataTransferObject;
using Presentation.DataTransferObject.Admin;
using Presentation.ResponseSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Admin
{
    public class BannerService : IBannerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BannerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IdenticalServiceResponse<bool>> CreateAsync(BannerModel model)
        {
            IdenticalServiceResponse<bool> response = new IdenticalServiceResponse<bool>();
            var IsExist = await _unitOfWork.BannerRepository.Any(x => x.Name.ToLower().Equals(model.Name) && x.ActivityStatus!.ToLower() != ActivityStatus.SOFT_DELETE.ToString().ToLower());
            if (!IsExist)
            {
                var entity = _mapper.Map<Banner>(model);
                await _unitOfWork.BannerRepository.AddAsync(entity);
                await _unitOfWork.SaveAsync();
                response.Result = true;
                return response;
            }
            response.Errors = $"{model.Name} already exist";
            return response;
        }

        public async Task<IdenticalServiceResponse<bool>> DeleteAsync(Guid id)
        {
            IdenticalServiceResponse<bool> response = new IdenticalServiceResponse<bool>();
            var IsExist = await _unitOfWork.BannerRepository.Any(x => x.Id == id && x.ActivityStatus!.ToLower() == ActivityStatus.SOFT_DELETE.ToString().ToLower());
            if (!IsExist)
            {
                response.Result = await _unitOfWork.BannerRepository.SoftDeleteAsync(id);
                return response;
            }
            response.Errors = ResponseMessage.DataNotFound.ToString();
            response.Result = false;
            return response;
        }

        public async Task<IdenticalServiceResponse<PaginationResponse<BannerModel>>> GetAllAsync(RequestPaginationModel pagination)
        {
            IdenticalServiceResponse<PaginationResponse<BannerModel>> response = new() { Result = new PaginationResponse<BannerModel>() };

            var Entities = _unitOfWork.BannerRepository
                .GetAll(predicate: x => x.ActivityStatus!.ToLower() != ActivityStatus.SOFT_DELETE.ToString().ToLower())
               .Skip((pagination.PageNumber - 1) * pagination.Size)
               .Take(pagination.Size).ToList();

            var totalData = await _unitOfWork.BannerRepository.CountAsync(x => x.ActivityStatus!.ToLower() != ActivityStatus.SOFT_DELETE.ToString().ToLower());

            response.Result.CurrentPageSize = pagination.PageNumber;
            response.Result.CurrentPageSize = pagination.Size;
            response.Result.TotalData = totalData;
            response.Result.TotaPages = totalData / pagination.Size + ((totalData % pagination.Size) == 0 ? 0 : 1);
            response.Result.ResultList = _mapper.Map<List<BannerModel>>(Entities);

            return response;
        }

        public async Task<IdenticalServiceResponse<BannerModel?>> GetByIdAsync(Guid id)
        {
            IdenticalServiceResponse<BannerModel?> response = new IdenticalServiceResponse<BannerModel?>();
            var IsEntity = await _unitOfWork.BannerRepository.Get(predicate: x => x.Id == id && x.ActivityStatus!.ToLower() != ActivityStatus.SOFT_DELETE.ToString().ToLower()).FirstOrDefaultAsync();

            if (IsEntity != null)
            {
                response.Result = _mapper.Map<BannerModel>(IsEntity);
                return response;
            }
            response.Errors = ResponseMessage.DataNotFound.ToString();
            return response;
        }

        public async Task<IdenticalServiceResponse<bool>> UpdateAsync(Guid id, BannerModel model)
        {
            IdenticalServiceResponse<bool> response = new IdenticalServiceResponse<bool>();
            var IsExist = await _unitOfWork.BannerRepository.FirstOrDefaultAsync(x => x.Id.Equals(id) && x.ActivityStatus!.ToLower() != ActivityStatus.SOFT_DELETE.ToString().ToLower());
            if (IsExist!=null)
            {
                IsExist.Name = model.Name;
                IsExist.Description = model.Description;
                IsExist.isActive=model.isActive;
                IsExist.Link = model.Link;
                IsExist.ActivityStatus=ActivityStatus.UPDATED.ToString();
                //var entity = _mapper.Map<Banner>(model);
                await _unitOfWork.BannerRepository.UpdateAsync(IsExist);
                response.Result = true;
                return response;
            }
            response.Errors = ResponseMessage.DataNotFound.ToString();
            response.Result = false;
            return response;
        }
    }
}
