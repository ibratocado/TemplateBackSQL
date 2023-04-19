using ApiTemplate.DTO.Request;

namespace ApiTemplate.Services.Interfaces
{
    public interface IAccountVerifyService
    {
        Task<Object> GetValidate(AccountRequest data);
    }
}
