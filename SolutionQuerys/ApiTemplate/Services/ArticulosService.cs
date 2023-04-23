using ApiTemplate.DTO;
using ApiTemplate.DTO.Request;
using ApiTemplate.DTO.Respon;
using ApiTemplate.Services.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace ApiTemplate.Services
{
    public class ArticulosService : IArticulosService
    {
        private readonly IDbContextService _contextDB;
        private SqlDataReader? _read;
        private readonly SqlCommand _command;
        public ArticulosService(IDbContextService context)
        {
            _contextDB = context;
            _command = new SqlCommand();
        }

        public async Task<GenericRespon> Delete(Guid id)
        {
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    var id = Guid.NewGuid();
                    _command.Connection = _contextDB.OpenConection();
                    _command.CommandText = "deleteArticulo";
                    _command.CommandType = CommandType.StoredProcedure;

                    _command.Parameters.AddWithValue("@id", id);

                    _command.ExecuteNonQuery();
                    _command.Parameters.Clear();
                    _command.Connection = _contextDB.CloseConection();

                });

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
            try
            {
                var totalRecords = await CountArticles(); //await _templateContext.Articulos.CountAsync();

                if (totalRecords <= 0)
                {
                    return new GenericRespon
                    {
                        State = 204,
                        Message = "No results",
                    };
                }

                
                var totalpage = totalRecords / length;

                if (totalpage == 0)
                    totalpage = 1;
                
                var paginator = new GenericPaginator<Articulo>
                {
                    Page = page,
                    RecordsByPage = length,
                    TotalRecords = totalRecords,
                    TotalPages = totalpage
                };
                
                var list = await Task<List<Articulo>>.Factory.StartNew(() => {
                    _command.Connection = _contextDB.OpenConection();
                    _command.CommandText = "selectArticulosPagination";
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.Parameters.AddWithValue("@page", page - 1);
                    _command.Parameters.AddWithValue("@length", length);

                    _read = _command.ExecuteReader();


                    var model = new List<Articulo>();
                    while (_read.Read())
                    {
                        var temp = new Articulo()
                        {
                            Id = _read.GetGuid(0),
                            Name = _read.GetString(1),
                            Price = _read.GetFloat(2),
                        };

                        model.Add(temp);
                    }
                    _command.Parameters.Clear();
                    _command.Connection = _contextDB.CloseConection();
                    return model;
                });

                paginator.Data = list;

                return new GenericRespon { Data = paginator, State = 200, Message = "Consultado Correctamente" };
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

        private Task<int> CountArticles()
        {
            return Task<int>.Factory.StartNew(() => {
                _command.Connection = _contextDB.OpenConection();
                _command.CommandText = "countArticles";
                _command.CommandType = CommandType.StoredProcedure;
                _read = _command.ExecuteReader();


                var model = 0;
                while (_read.Read())
                {
                    model = _read.GetInt32(0);
                }
                _command.Parameters.Clear();
                _command.Connection = _contextDB.CloseConection();
                return model;
            });
        }

        public Task<GenericRespon> GetOne(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<GenericRespon> Insert(ArticlesPostRequest model)
        {

            try
            {
            
                await Task.Factory.StartNew(() =>
                {
                    var id = Guid.NewGuid();
                    _command.Connection = _contextDB.OpenConection();
                    _command.CommandText = "addArticulo";
                    _command.CommandType = CommandType.StoredProcedure;

                    _command.Parameters.AddWithValue("@id", Guid.NewGuid());
                    _command.Parameters.AddWithValue("@name", model.Name);
                    _command.Parameters.AddWithValue("@price", model.Price);

                    _command.ExecuteNonQuery();
                    _command.Parameters.Clear();
                    _command.Connection = _contextDB.CloseConection();

                });

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
            try
            {
                var exits = await ExistArticel(model.Id);

                if (!exits)
                {
                    return new GenericRespon
                    {
                        State = 204,
                        Message = "Datos no Validos",
                    };
                }

                await Task.Factory.StartNew(() =>
                {
                    var id = Guid.NewGuid();
                    _command.Connection = _contextDB.OpenConection();
                    _command.CommandText = " updateArticulo";
                    _command.CommandType = CommandType.StoredProcedure;

                    _command.Parameters.AddWithValue("@id", Guid.NewGuid());
                    _command.Parameters.AddWithValue("@name", model.Name);
                    _command.Parameters.AddWithValue("@price", model.Price);

                    _command.ExecuteNonQuery();
                    _command.Parameters.Clear();
                    _command.Connection = _contextDB.CloseConection();

                });

                return new GenericRespon
                {
                    State = 200,
                    Message = "Modificado Correctamente",
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

        private Task<bool> ExistArticel(Guid id) 
        {
            return Task<bool>.Factory.StartNew(() => {
                _command.Connection = _contextDB.OpenConection();
                _command.CommandText = "deleteArticulo";
                _command.CommandType = CommandType.StoredProcedure;
                _command.Parameters.AddWithValue("@id", id);
                _read = _command.ExecuteReader();


                var model = false;
                while (_read.Read())
                {
                    model = _read.GetBoolean(0);
                }
                _command.Parameters.Clear();
                _command.Connection = _contextDB.CloseConection();
                return model;
            });
        }
    }
}
