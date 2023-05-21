using ApiTemplate.DTO;
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

        public async Task<GenericRespon> Add(CustomerAddRequest model)
        {
            GenericRespon respon = new GenericRespon();
            try
            {
                if (await _context.Customers.AnyAsync(i => i.Name == model.Name && i.LastNames == model.LastNames))
                {
                    respon.State = StatusCodes.Status204NoContent;
                    respon.Message = "Registro Existente";
                    return respon;
                }
                Customer custommer = new Customer()
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    Account = model.Account,
                    LastNames = model.LastNames,
                    Addres = model.Addres,
                    Active = true
                };

                _context.Add(custommer);
                await _context.SaveChangesAsync();

                respon.State = StatusCodes.Status200OK;
                respon.Message = "Agregado Con Exito";
                respon.Data = custommer;

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
                var customer = await _context.Customers.FirstOrDefaultAsync(i => i.Id == id && i.Active == true);
                if (customer == null)
                {
                    respon.State = StatusCodes.Status204NoContent;
                    respon.Message = "Datos Erroneos";
                    return respon;
                }

                customer.Active = false;

                _context.Entry(customer).Property(i => i.Active).IsModified = true;
                await _context.SaveChangesAsync();

                respon.State = StatusCodes.Status200OK;
                respon.Message = "Eliminado Con Exito";
                respon.Data = customer;

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
                var customer = await _context.Customers.FirstOrDefaultAsync(i => i.Id == id && i.Active == true);
                if (customer == null)
                {
                    respon.State = StatusCodes.Status204NoContent;
                    respon.Message = "Datos Erroneos";
                    return respon;
                }

                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();

                respon.State = StatusCodes.Status200OK;
                respon.Message = "Eliminado Con Exito";
                respon.Data = customer;

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

        public async Task<GenericRespon> GetFull(GenricPaginatorN paginator)
        {
            GenericRespon respon = new GenericRespon();

            try
            {
                var list = await _context.Customers.Where(i => i.Active == true).
                    Skip(paginator.Page).
                    Take(paginator.RecordsByPage).
                    OrderBy(i => i.Name).ToListAsync();
                if (list == null)
                {
                    respon.State = StatusCodes.Status204NoContent;
                    respon.Message = "No Se Encontraron resultados";
                    return respon;
                }

                var totalRecord = await _context.Customers.CountAsync(i => i.Active == true);

                GenericPaginatorRespon<Customer> paginatorRespon = new GenericPaginatorRespon<Customer>()
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

        public async Task<GenericRespon> Update(CustomerUpdateRequest model)
        {
            GenericRespon respon = new GenericRespon();
            try
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(i => i.Id == model.Id && i.Active == true);
                if (customer == null)
                {
                    respon.State = StatusCodes.Status204NoContent;
                    respon.Message = "Datos Erroneos";
                    return respon;
                }

                customer.Name = model.Name;
                customer.LastNames = model.LastNames;
                customer.Addres = model.Addres;

                _context.Entry(customer).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                respon.State = StatusCodes.Status200OK;
                respon.Message = "Actualizado Con Exito";
                respon.Data = customer;

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
