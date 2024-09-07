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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IdenticalServiceResponse<bool>> CreateAsync(CategoryModel model)
        {
            IdenticalServiceResponse<bool> response = new IdenticalServiceResponse<bool>();
            var IsExist = await _unitOfWork.CategoryRepository.Any(x => x.Name.ToLower().Equals(model.Name) && x.ActivityStatus!.ToLower() != ActivityStatus.SOFT_DELETE.ToString().ToLower());
            if (!IsExist)
            {
                var entity = _mapper.Map<Category>(model);
                await _unitOfWork.CategoryRepository.AddAsync(entity);
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
            var IsExist = await _unitOfWork.CategoryRepository.Any(x => x.Id == id && x.ActivityStatus!.ToLower() == ActivityStatus.SOFT_DELETE.ToString().ToLower());
            if (!IsExist)
            {
                response.Result = await _unitOfWork.CategoryRepository.SoftDeleteAsync(id);
                return response;
            }
            response.Errors = ResponseMessage.DataNotFound.ToString();
            response.Result = false;
            return response;
        }

        public async Task<IdenticalServiceResponse<PaginationResponse<CategoryModel>>> GetAllAsync(RequestPaginationModel pagination)
        {
            IdenticalServiceResponse<PaginationResponse<CategoryModel>> response = new() { Result = new PaginationResponse<CategoryModel>() };

            var Entities = _unitOfWork.CategoryRepository
                .GetAll(predicate: x => x.ActivityStatus!.ToLower() != ActivityStatus.SOFT_DELETE.ToString().ToLower())
               .Skip((pagination.PageNumber - 1) * pagination.Size)
               .Take(pagination.Size).ToList();

            var totalData = await _unitOfWork.CategoryRepository.CountAsync(x => x.ActivityStatus!.ToLower() != ActivityStatus.SOFT_DELETE.ToString().ToLower());

            response.Result.CurrentPageSize = pagination.PageNumber;
            response.Result.CurrentPageSize = pagination.Size;
            response.Result.TotalData = totalData;
            response.Result.TotaPages = totalData / pagination.Size + ((totalData % pagination.Size) == 0 ? 0 : 1);
            response.Result.ResultList = _mapper.Map<List<CategoryModel>>(Entities);

            return response;
        }

        public async Task<IdenticalServiceResponse<CategoryModel?>> GetByIdAsync(Guid id)
        {
            IdenticalServiceResponse<CategoryModel?> response = new IdenticalServiceResponse<CategoryModel?>();
            var IsEntity = await _unitOfWork.CategoryRepository.Get(predicate: x => x.Id == id && x.ActivityStatus!.ToLower() != ActivityStatus.SOFT_DELETE.ToString().ToLower()).FirstOrDefaultAsync();

            if (IsEntity != null)
            {
                response.Result = _mapper.Map<CategoryModel>(IsEntity);
                return response;
            }
            response.Errors = ResponseMessage.DataNotFound.ToString();
            return response;
        }

        public async Task<IdenticalServiceResponse<bool>> UpdateAsync(Guid id, CategoryModel model)
        {
            IdenticalServiceResponse<bool> response = new IdenticalServiceResponse<bool>();
            var IsExist = await _unitOfWork.CategoryRepository.Any(x => x.Id.Equals(id) && x.ActivityStatus!.ToLower() != ActivityStatus.SOFT_DELETE.ToString().ToLower());
            if (IsExist)
            {
                var entity = _mapper.Map<Category>(model);
                await _unitOfWork.CategoryRepository.UpdateAsync(entity);
                response.Result = true;
                return response;
            }
            response.Errors = ResponseMessage.DataNotFound.ToString();
            response.Result = false;
            return response;
        }
    }
}
