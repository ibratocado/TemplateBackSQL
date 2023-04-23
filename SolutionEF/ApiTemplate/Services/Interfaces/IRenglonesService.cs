using ApiTemplate.DTO.Respon;

namespace ApiTemplate.Services.Interfaces
{
    public interface IRenglonesService
    {
        Task<GenericRespon> Get(int number);
        Task<GenericRespon> GetRescursive(int number);
    }
}
