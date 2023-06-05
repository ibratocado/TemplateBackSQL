using ApiTemplate.DTO.Respon;

namespace ApiTemplate.Services.Interfaces
{
    public interface IRolsService
    {
        Task<GenericRespon> GetFullRols();
    }
}
