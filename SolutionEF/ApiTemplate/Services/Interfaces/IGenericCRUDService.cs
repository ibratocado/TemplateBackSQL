using ApiTemplate.DTO.Respon;

namespace ApiTemplate.Services.Interfaces
{
    public interface Interface<A,U,P,C>
    {
        Task<GenericRespon> GetFull();
        Task<GenericRespon> GetByPage(P paginator);
        Task<GenericRespon> GetByFilter(P filterPaginator);
        Task<GenericRespon> GetById(C id);
        Task<GenericRespon> DeleteLogic(C id);
        Task<GenericRespon> DeletePhysical(C id);
        Task<GenericRespon> Add(A model);
        Task<GenericRespon> Update(U model);
    }
}
