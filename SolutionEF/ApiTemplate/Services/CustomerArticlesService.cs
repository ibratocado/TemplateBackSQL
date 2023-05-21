using ApiTemplate.DTO.Request;
using ApiTemplate.DTO.Respon;
using ApiTemplate.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Services
{
    public class CustomerArticlesService : IGenericCRUD<CustomerArticleAddRequest, CustomerArticleUpdateRequest>
    {
        private readonly Db_TemplateContext _context;

        public CustomerArticlesService(Db_TemplateContext context)
        {
            _context = context;
        }

        public async Task<GenericRespon> Add(CustomerArticleAddRequest model)
        {
            GenericRespon respon = new GenericRespon();
            try
            {
                foreach (var item in model.Articles)
                {
                    var artStock = await _context.Articles.FirstOrDefaultAsync(i=> i.Id == item);

                    if (!await _context.CuatomerArticles.
                        AnyAsync(i => i.Cuatomer == model.Cuatomer && 
                        i.Article == item) && artStock != null)
                    {
                        CuatomerArticle article = new CuatomerArticle()
                        {
                            Id = Guid.NewGuid(),
                            Cuatomer = model.Cuatomer,
                            Article = item,
                            Date = DateTime.Now
                        };

                        artStock.Stock --;

                        _context.Entry(artStock).Property(i => i.Stock).IsModified = true;
                        await _context.SaveChangesAsync();

                        _context.Add(article);
                        await _context.SaveChangesAsync();
                    }
                }

                respon.State = StatusCodes.Status200OK;
                respon.Message = "Comprados";

                return respon;

            }
            catch (Exception)
            {
                respon.State = StatusCodes.Status503ServiceUnavailable;
                respon.Message = "Error De Servicios";
                return respon;
            }
        }

        public Task<GenericRespon> DeleteLogic(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericRespon> DeletePhysical(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<GenericRespon> GetById(Guid id, GenericPaginatorRequest? paginator)
        {
            GenericRespon respon = new GenericRespon();

            try
            {
                var list = await _context.CuatomerArticles.
                    Where(i => i.Cuatomer == id).OrderBy(i => i.Date).Include(i => i.ArticleNavigation).ToListAsync();
                if (list == null)
                {
                    respon.State = StatusCodes.Status204NoContent;
                    respon.Message = "No Se Encontraron resultados";
                    return respon;
                }

                respon.State = StatusCodes.Status200OK;
                respon.Message = "Consultado Con Exito";
                respon.Data = list;

                return respon;
            }
            catch (Exception)
            {
                respon.State = StatusCodes.Status503ServiceUnavailable;
                respon.Message = "Error De Servicios";
                return respon;
            }
        }

        public Task<GenericRespon> GetFull(GenricPaginatorN paginator)
        {
            throw new NotImplementedException();
        }

        public Task<GenericRespon> Update(CustomerArticleUpdateRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
