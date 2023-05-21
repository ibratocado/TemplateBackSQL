using ApiTemplate.DTO;
using ApiTemplate.DTO.Request;
using ApiTemplate.DTO.Respon;
using ApiTemplate.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Services
{
    public class ArticleService : IGenericCRUD<ArticlesAddRequest, ArticlesUpdateRequest>
    {
        private readonly Db_TemplateContext _context;
        private readonly IFileService _fileService;

        public ArticleService(Db_TemplateContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<GenericRespon> Add(ArticlesAddRequest model)
        {
            GenericRespon respon = new GenericRespon();
            try
            {
                if (await _context.Articles.AnyAsync(i => i.Code == model.Code))
                {
                    respon.State = StatusCodes.Status204NoContent;
                    respon.Message = "Registro Existente";
                    return respon;
                }

                var image = _fileService.SaveFile(model.Image, "Articles", "Images");

                if (image.Length <= 0)
                {
                    respon.State = StatusCodes.Status409Conflict;
                    respon.Message = "Error al guardar la imagen";
                    return respon;
                }

                Article article = new Article()
                {
                    Id = Guid.NewGuid(),
                    Code = model.Code,
                    Description = model.Description,
                    Price = model.Price,
                    Stock = model.Stock,
                    Image = image,
                    Active = true
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

        public async Task<GenericRespon> DeleteLogic(Guid id)
        {
            GenericRespon respon = new GenericRespon();
            try
            {
                var article = await _context.Articles.FirstOrDefaultAsync(i => i.Id == id && i.Active == true);
                if (article == null)
                {
                    respon.State = StatusCodes.Status409Conflict;
                    respon.Message = "Datos Erroneos";
                    return respon;
                }

                article.Active = false;

                _context.Entry(article).Property(i => i.Active).IsModified = true;
                await _context.SaveChangesAsync();

                respon.State = StatusCodes.Status200OK;
                respon.Message = "Eliminado Con Exito";
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

        public async Task<GenericRespon> DeletePhysical(Guid id)
        {
            GenericRespon respon = new GenericRespon();
            try
            {
                var article = await _context.Articles.FirstOrDefaultAsync(i => i.Id == id && i.Active == true);
                if (article == null)
                {
                    respon.State = StatusCodes.Status409Conflict;
                    respon.Message = "Datos Erroneos";
                    return respon;
                }

                string[] file = article.Image.Split('/');

                _fileService.DeleteFile(file[0], file[2], file[1]);

                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();

                respon.State = StatusCodes.Status200OK;
                respon.Message = "Eliminado Con Exito";
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

        public async Task<GenericRespon> GetById(Guid id, GenericPaginatorRequest? paginator)
        {
            GenericRespon respon = new GenericRespon();

            try
            {
                var list = await _context.Articles.Where(i => i.Active == true).FirstOrDefaultAsync(i => i.Id == id);
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

        public async Task<GenericRespon> GetFull(GenricPaginatorN paginator)
        {
            GenericRespon respon = new GenericRespon();

            try
            {
                var list = await _context.Articles.Where(i => i.Active == true).
                    Skip(paginator.Page).
                    Take(paginator.RecordsByPage).
                    OrderBy(i => i.Code).ToListAsync();
                if (list.Count < 0)
                {
                    respon.State = StatusCodes.Status204NoContent;
                    respon.Message = "No Se Encontraron resultados";
                    return respon;
                }

                var totalRecord = await _context.Articles.CountAsync(i => i.Active == true);

                GenericPaginatorRespon<Article> paginatorRespon = new GenericPaginatorRespon<Article>()
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

        public async Task<GenericRespon> Update(ArticlesUpdateRequest model)
        {
            GenericRespon respon = new GenericRespon();
            try
            {
                var article = await _context.Articles.FirstOrDefaultAsync(i => i.Id == model.Id && i.Active == true);
                if (article == null)
                {
                    respon.State = StatusCodes.Status204NoContent;
                    respon.Message = "Datos Erroneos";
                    return respon;
                }
               
                if(model.Image != null)
                {
                    string[] file = article.Image.Split('/');

                    var updateFile = _fileService.UpdateFile(model.Image, file[0], file[2], file[1]);

                    if (!updateFile)
                    {
                        respon.State = StatusCodes.Status409Conflict;
                        respon.Message = "Error al actualizar la imagen";
                        return respon;
                    }
                }

                article.Description = model.Description;
                article.Stock = model.Stock;
                article.Price = model.Price;

                _context.Entry(article).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                respon.State = StatusCodes.Status200OK;
                respon.Message = "Actualizado Con Exito";
                respon.Data = article;

                return respon;

            }
            catch (Exception e)
            {
                respon.State = StatusCodes.Status503ServiceUnavailable;
                respon.Message = "Error De Servicios";
                respon.Data = e;
                return respon;
            }
        }
    }
}
