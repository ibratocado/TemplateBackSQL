using ApiTemplate.Services.Interfaces;

namespace ApiTemplate.Services
{
    public class ExampleService : IExampleService
    {
        /*private readonly DbContext _context;

        public ExampleService(DbContext context)
        {
            _context = context;
        }*/

        public string Add(object model)
        {
            /*var add = new Inventary()
            {
                ArticleId = model.productId,
                StoreId = model.storeId,
                Date = DateTime.Now
            };

            _context.Add(add);
            await _context.SaveChangesAsync();*/
            
            throw new NotImplementedException();
        }

        public string Delete(object model)
        {
            /*var inventarie = await _context.Inventaries.FindAsync(id);
            if (inventarie != null)
            {
                _context.Inventaries.Remove(inventarie);
                await _context.SaveChangesAsync();
                return "Eliminado Correctamente";
            }

            return "Usuario No Encontrado";*/
            throw new NotImplementedException();
        }

        public bool ExitsById(object id)
        {
            //return _context.Inventaries.Any(i => i.Id == id);
            throw new NotImplementedException();
        }

        public object Select()
        {
            /*var inventarie = await _context.Inventaries.ToListAsync();
            return inventarie;*/

            throw new NotImplementedException();
        }

        public object SelectById(object id)
        {
            /*var inventarie = await _context.Inventaries.Include(i => i.Article).FirstOrDefaultAsync(i => i.Id == id);*/
            throw new NotImplementedException();
        }

        public string Update(object model)
        {
            /*if (exist(data.id))
            {
                var inventary = new Inventary()
                {
                    Id = data.id,
                    ArticleId = data.productId,
                    StoreId = data.storeId,
                    Date = DateTime.Now
                };
                _context.Entry(inventary).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return "Actualizado Correctamente";
            }

            return "Usuario No Encontrado";*/
            throw new NotImplementedException();
        }
    }
}
