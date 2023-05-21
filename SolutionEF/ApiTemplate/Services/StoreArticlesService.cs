using ApiTemplate.DTO;
using ApiTemplate.DTO.Request;
using ApiTemplate.DTO.Respon;
using ApiTemplate.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Services
{
    public class StoreArticlesService : IGenericCRUD<StoreArticleAddRequest, StoreArticleUpdateRequest>
    {
        private readonly Db_TemplateContext _context;

        public StoreArticlesService(Db_TemplateContext context)
        {
            _context = context;
        }

        public async Task<GenericRespon> Add(StoreArticleAddRequest model)
        {
            GenericRespon respon = new GenericRespon();
            try
            {
                if (await _context.StoreArticles.AnyAsync(i => i.Store == model.Store && i.Article == model.Article))
                {
                    respon.State = StatusCodes.Status204NoContent;
                    respon.Message = "Registro Existente";
                    return respon;
                }

                StoreArticle article = new StoreArticle()
                {
                    Id = Guid.NewGuid(),
                    Store = model.Store,
                    Article = model.Article,
                    Date = DateTime.Now
                };

                _context.Add(article);
                await _context.SaveChangesAsync();

                respon.State = StatusCodes.Status200OK;
                respon.Message = "Agregado Con Exito";
                respon.Data = article;

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
                var list = await _context.StoreArticles.
                    Skip(paginator.Page).
                    Include(i => i.ArticleNavigation).OrderBy(i => i.Date).
                    Where(i => i.Store == id && i.ArticleNavigation.Active == true)
                    .Take(paginator.RecordsByPage).ToListAsync();

                if (list == null)
                {
                    respon.State = StatusCodes.Status204NoContent;
                    respon.Message = "No Se Encontraron resultados";
                    return respon;
                }
                var totalRecord = await _context.StoreArticles.Include(i=> i.ArticleNavigation).
                    CountAsync(i => i.Store == id && i.ArticleNavigation.Active == true);
                GenericPaginatorRespon<StoreArticle> paginatorRespon = new GenericPaginatorRespon<StoreArticle>()
                {
                    Page = paginator.Page,
                    RecordsByPage = paginator.RecordsByPage,
                    TotalRecords = totalRecord,
                    TotalPages = totalRecord / paginator.RecordsByPage,
                    Data = list
                };

                respon.State = StatusCodes.Status200OK;
                respon.Message = "Consultado Con Exito";
                respon.Data = paginatorRespon;

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

        public Task<GenericRespon> Update(StoreArticleUpdateRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
