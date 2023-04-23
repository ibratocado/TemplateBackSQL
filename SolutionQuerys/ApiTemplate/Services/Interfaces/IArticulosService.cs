using ApiTemplate.DTO.Request;
using ApiTemplate.DTO.Respon;

namespace ApiTemplate.Services.Interfaces
{
    public interface IArticulosService
    {
        Task<GenericRespon> GetAll(int page, int length);
        Task<GenericRespon> GetOne(Guid id);
        Task<GenericRespon> Insert(ArticlesPostRequest model);
        Task<GenericRespon> Update(ArticlesPutRequest model);
        Task<GenericRespon> Delete(Guid id);


    }
}
