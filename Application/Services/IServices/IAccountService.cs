using Presentation.DataTransferObject;
using Presentation.ResponseSchema;

namespace Application.Services.IServices
{
    public interface IAccountService
    {
        Task<IdenticalServiceResponse<bool>> CreateUserAccountAsync(RegisterUserModel model);
        Task<IdenticalServiceResponse<AuthResponse>> SignInUserAccountAsync(LoginUserModel model);
        Task<IdenticalServiceResponse<string>> SignOutUserAccountAsync(Guid id);
    }
}
