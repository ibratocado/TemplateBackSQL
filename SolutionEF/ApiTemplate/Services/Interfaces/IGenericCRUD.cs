using ApiTemplate.DTO.Request;
using ApiTemplate.DTO.Respon;

namespace ApiTemplate.Services.Interfaces
{
    public interface IGenericCRUD<A,U> 
    {
        Task<GenericRespon> GetFull(GenricPaginatorN paginator = null);
        Task<GenericRespon> GetById(Guid id, GenericPaginatorRequest? paginator = null);
        Task<GenericRespon> DeleteLogic(Guid id);
        Task<GenericRespon> DeletePhysical(Guid id);
        Task<GenericRespon> Add(A model);
        Task<GenericRespon> Update(U model);

    }
}
