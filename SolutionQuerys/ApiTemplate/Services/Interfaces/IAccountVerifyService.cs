using ApiTemplate.DTO.Request;
using ApiTemplate.DTO.Respon;

namespace ApiTemplate.Services.Interfaces
{
    public interface IAccountVerifyService
    {
        Task<GenericRespon> GetValidate(AccountRequest data);
    }
}
