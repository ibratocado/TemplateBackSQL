using ApiTemplate.DTO.Request;
using ApiTemplate.DTO.Respon;
using ApiTemplate.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Services
{
    public class StoreService : IGenericCRUD<StoreAddRequest, StoreUpdateRequest> 
    {
        private readonly Db_TemplateContext _context;

        public StoreService(Db_TemplateContext context)
        {
            _context = context;
        }

        public async Task<GenericRespon> Add(StoreAddRequest model)
        {
            GenericRespon respon = new GenericRespon();
            try
            {
                if (await _context.Stores.AnyAsync(i => i.Branch == model.Branch && i.Addres == model.Addres))
                {
                    respon.State = StatusCodes.Status204NoContent;
                    respon.Message = "Registro Existente";
                    return respon;
                }
                Store store = new Store()
                {
                    Id = Guid.NewGuid(),
                    Branch = model.Branch,
                    Addres = model.Addres,
                    Active = true
                };

                _context.Add(store);
                await _context.SaveChangesAsync();

                respon.State = StatusCodes.Status200OK;
                respon.Message = "Agregado Con Exito";
                respon.Data = store;

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
                var store = await _context.Stores.FirstOrDefaultAsync(i => i.Id == id && i.Active == true);
                if (store == null)
                {
                    respon.State = StatusCodes.Status204NoContent;
                    respon.Message = "Datos Erroneos";
                    return respon;
                }

                store.Active = false;

                _context.Entry(store).Property(i => i.Active).IsModified = true;
                await _context.SaveChangesAsync();

                respon.State = StatusCodes.Status200OK;
                respon.Message = "Eliminado Con Exito";
                respon.Data = store;

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
                var store = await _context.Stores.FirstOrDefaultAsync(i => i.Id == id && i.Active == true);
                if (store == null)
                {
                    respon.State = StatusCodes.Status204NoContent;
                    respon.Message = "Datos Erroneos";
                    return respon;
                }

                _context.Stores.Remove(store);
                await _context.SaveChangesAsync();

                respon.State = StatusCodes.Status200OK;
                respon.Message = "Eliminado Con Exito";
                respon.Data = store;

                return respon;

            }
            catch (Exception)
            {
                respon.State = StatusCodes.Status503ServiceUnavailable;
                respon.Message = "Error De Servicios";
                return respon;
            }
        }

        public async Task<GenericRespon> GetById(Guid id)
        {
            GenericRespon respon = new GenericRespon();

            try
            {
                var list = await _context.Stores.Where(i => i.Active == true).FirstOrDefaultAsync(i => i.Id == id);
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

        public async Task<GenericRespon> GetFull()
        {
            GenericRespon respon  = new GenericRespon();

            try
            {
                var list = await _context.Stores.Where(i => i.Active == true).OrderBy(i => i.Branch).ToListAsync();
                if (list.Count < 0)
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

        public async Task<GenericRespon> Update(StoreUpdateRequest model)
        {
            GenericRespon respon = new GenericRespon();
            try
            {
                var store = await _context.Stores.FirstOrDefaultAsync(i => i.Id == model.Id && i.Active == true);
                if (store == null)
                {
                    respon.State = StatusCodes.Status204NoContent;
                    respon.Message = "Datos Erroneos";
                    return respon;
                }

                store.Branch = model.Branch;
                store.Addres = model.Addres;

                _context.Entry(store).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                respon.State = StatusCodes.Status200OK;
                respon.Message = "Actualizado Con Exito";
                respon.Data = store;

                return respon;

            }
            catch (Exception)
            {
                respon.State = StatusCodes.Status503ServiceUnavailable;
                respon.Message = "Error De Servicios";
                return respon;
            }
        }
    }
}
