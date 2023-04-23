using ApiTemplate.DTO;
using ApiTemplate.DTO.Request;
using ApiTemplate.DTO.Respon;
using ApiTemplate.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Services
{
    public class ArticulosService : IArticulosService
    {
        private readonly Db_TemplateContext _templateContext;
  
        public ArticulosService(Db_TemplateContext templateContext)
        {
            _templateContext = templateContext;
        }

        public async Task<GenericRespon> Delete(Guid id)
        {
            try
            {
                var articulo = await _templateContext.Articulos.FindAsync(id);
                if (articulo == null)
                {
                    return new GenericRespon
                    {
                        State = 204,
                        Message = "No results",
                    };
                }

                _templateContext.Articulos.Remove(articulo);
                await _templateContext.SaveChangesAsync();

                return new GenericRespon
                {
                    State = 200,
                    Message = "Elinimado Correctamente",
                };
            }
            catch (Exception)
            {
                return new GenericRespon
                {
                    State = 503,
                    Message = "Error de Servicios",
                };
            }
        }

        public async Task<GenericRespon> GetAll(int page, int length)
        {
           
            if (_templateContext.Articulos == null)
            {
                return new GenericRespon
                {
                    State = 204,
                    Message = "No results",
                };
            }

            var totalRecords = await _templateContext.Articulos.CountAsync();
            var totalpage = totalRecords / length;
            if(totalpage == 0)
                totalpage = 1;
            var paginator = new GenericPaginator<Articulo>
            {
                Page = page,
                RecordsByPage = length,
                TotalRecords = totalRecords,
                TotalPages = totalpage
            };
            var list = await _templateContext.Articulos.OrderBy(i=> i.Name).Skip((page - 1) * length).Take(length).ToListAsync();

            paginator.Data = list;

            return new GenericRespon { Data = paginator, State = 200, Message = "Consultado Correctamente" }; 
        }

        public async Task<GenericRespon> GetOne(Guid id)
        {
            var articulo = await _templateContext.Articulos.FindAsync(id);

            if (articulo == null)
            {
                return new GenericRespon
                {
                    State = 204,
                    Message = "No results",
                };
            }

            return new GenericRespon { State = 200, Data = articulo, Message = "Consultado Correctamente"};
        }

        public async Task<GenericRespon> Insert(ArticlesPostRequest model)
        {

            try
            {
                var articulo = new Articulo
                {
                    Name = model.Name,
                    Price = model.Price
                };

                _templateContext.Articulos.Add(articulo);
                await _templateContext.SaveChangesAsync();

                return new GenericRespon
                {
                    State = 200,
                    Message = "Agregado Correctamente",
                };
            }
            catch (Exception)
            {
                return new GenericRespon
                {
                    State = 503,
                    Message = "Error de Servicios",
                };
            }

            
        }

        public async Task<GenericRespon> Update(ArticlesPutRequest model)
        {
            var exits = await _templateContext.Articulos.AnyAsync(i => i.Id == model.Id);
            if (!exits)
            {
                return new GenericRespon
                {
                    State = 204,
                    Message = "Datos no Validos",
                };
            }

            var articulo = new Articulo 
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price
            };

            _templateContext.Entry(articulo).State = EntityState.Modified;

            try
            {
                await _templateContext.SaveChangesAsync();
                return new GenericRespon
                {
                    State = 200,
                    Message = "Modificado Correctamente",
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                return new GenericRespon
                {
                    State = 503,
                    Message = "Error de Servicios",
                };
            }
        }
    }
}
