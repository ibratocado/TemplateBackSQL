using ApiTemplate.DTO.Request;
using ApiTemplate.DTO.Respon;
using ApiTemplate.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Services
{
    public class CustomerService : IGenericCRUD<CustomerAddRequest, CustomerUpdateRequest>
    {
        private readonly Db_TemplateContext _context;

        public CustomerService(Db_TemplateContext context)
        {
            _context = context;
        }

        public Task<GenericRespon> Add(CustomerAddRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<GenericRespon> DeleteLogic(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericRespon> DeletePhysical(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<GenericRespon> GetById(Guid id)
        {
            GenericRespon respon = new GenericRespon();

            try
            {
                var list = await _context.Customers.FirstOrDefaultAsync(i => i.Id == id);
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
            GenericRespon respon = new GenericRespon();

            try
            {
                var list = await _context.Customers.Where(i => i.Active == true).OrderBy(i => i.Name).ToListAsync();
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

        public Task<GenericRespon> Update(CustomerUpdateRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
