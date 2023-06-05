using ApiTemplate.DTO.Request;
using ApiTemplate.DTO.Respon;
using ApiTemplate.Services.Interfaces;
using ModelsDB;
using System.Data;
using System.Data.SqlClient;

namespace ApiTemplate.Services
{
    public class UsersService : IGenericCRUDService<AddUserRequest, UpdateUserRequest, GenericPaginatorRequest<string>, Guid>
    {

        private readonly IDbContextService _contextDB;
        private SqlDataReader? _read;
        private readonly SqlCommand _command;

        public UsersService(IDbContextService contextDB)
        {
            _contextDB = contextDB;
            _command = new SqlCommand();
        }

        public async Task<GenericRespon> Add(AddUserRequest model)
        {
            GenericRespon genericRespon = new GenericRespon();
            try
            {
                var task = await Task<GenericRespon>.Factory.StartNew(() =>
                {
                    var exist = ExistCurp(model.Curp.ToUpper());
                    if (exist)
                    {
                        genericRespon.State = StatusCodes.Status409Conflict;
                        genericRespon.Message = "Datos Repetidos";
                        genericRespon.Data = model.Curp;
                    }

                    var idAccount = Guid.NewGuid();

                    if (!AddAccount(model, idAccount))
                    {
                        genericRespon.State = StatusCodes.Status503ServiceUnavailable;
                        genericRespon.Message = "Error Al Crear la Cuenta ";
                        return genericRespon;
                    }

                    var id = Guid.NewGuid();
                    _command.Connection = _contextDB.OpenConection();
                    _command.CommandText = "addUser";
                    _command.CommandType = CommandType.StoredProcedure;

                    _command.Parameters.AddWithValue("@idAccount", idAccount);
                    _command.Parameters.AddWithValue("@idUser", id);
                    _command.Parameters.AddWithValue("@curp", model.Curp.ToUpper());
                    _command.Parameters.AddWithValue("@name", model.Name.ToUpper());
                    _command.Parameters.AddWithValue("@lastName", model.LastName.ToUpper());
                    _command.Parameters.AddWithValue("@secondLastName", model.SecondLastName.ToUpper());
                    _command.Parameters.AddWithValue("@salary", model.Salary);
                    _command.Parameters.AddWithValue("@phone", model.Phone);

                    _command.ExecuteNonQuery();
                    _command.Parameters.Clear();
                    _command.Connection = _contextDB.CloseConection();

                    genericRespon.State = StatusCodes.Status200OK;
                    genericRespon.Message = "Agregado correctamente";
                    genericRespon.Data = id;
                    return genericRespon;
                });

                return task;
            }
            catch (Exception)
            {
                genericRespon.State = StatusCodes.Status503ServiceUnavailable;
                genericRespon.Message = "Error de Servicios";
                return genericRespon;
            }
        }

        private bool AddAccount(AddUserRequest model,Guid id)
        {
            try
            {
                _command.Connection = _contextDB.OpenConection();
                _command.CommandText = "addAccount";
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.AddWithValue("@idAccount", id);
                _command.Parameters.AddWithValue("@roleId", model.RoleId);
                _command.Parameters.AddWithValue("@acount", model.Account);
                _command.Parameters.AddWithValue("@pount", model.pount);

                _command.ExecuteNonQuery();
                _command.Parameters.Clear();
                _command.Connection = _contextDB.CloseConection();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<GenericRespon> DeleteLogic(Guid id)
        {
            GenericRespon genericRespon = new GenericRespon();
            try
            {

                var exits = ExistUser(id);

                if (!exits)
                {
                    genericRespon.State = StatusCodes.Status406NotAcceptable;
                    genericRespon.Message = "Datos Incorrectos";
                    return genericRespon;
                }

                var task = await Task<GenericRespon>.Factory.StartNew(() =>
                {
                    _command.Connection = _contextDB.OpenConection();
                    _command.CommandText = "DeleteLogcicUser";
                    _command.CommandType = CommandType.StoredProcedure;

                    _command.Parameters.AddWithValue("@idUser", id);

                    _command.ExecuteNonQuery();
                    _command.Parameters.Clear();
                    _command.Connection = _contextDB.CloseConection();

                    genericRespon.State = StatusCodes.Status200OK;
                    genericRespon.Message = "Desactivado Correctamente";

                    return genericRespon;

                });

                return task;
            }
            catch (Exception)
            {
                genericRespon.State = StatusCodes.Status503ServiceUnavailable;
                genericRespon.Message = "Error de Servicios";
                return genericRespon;
            }
        }

        public Task<GenericRespon> DeletePhysical(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericRespon> GetByFilter(GenericPaginatorRequest<string> filterPaginator)
        {
            throw new NotImplementedException();
        }

        public async Task<GenericRespon> GetById(Guid id)
        {
            GenericRespon genericRespon = new GenericRespon();
            try
            {
                var task = await Task<GenericRespon>.Factory.StartNew(() => {

                    _command.Connection = _contextDB.OpenConection();
                    _command.CommandText = "SelectOneUser";
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.Parameters.AddWithValue("@id", id);
                    _read = _command.ExecuteReader();

                    UserModel temp = null;
                    while (_read.Read())
                    {
                        temp = new UserModel()
                        {
                            Id = _read.GetGuid(0),
                            Acount = _read.GetString(3),
                            Role = _read.GetString(1),
                            RoleId = _read.GetInt32(2),
                            Curp = _read.GetString(4),
                            LastName = _read.GetString(5),
                            SecondLastName = _read.GetString(6),
                            Name = _read.GetString(7),
                            Salary = _read.GetDouble(8),
                            Phone = _read.GetString(9),

                        };

                    }
                    _command.Parameters.Clear();
                    _command.Connection = _contextDB.CloseConection();

                    GenericPagiantorRespon<UserModel> paginatorRespon = new GenericPagiantorRespon<UserModel>();

                    if (temp == null)
                    {
                        genericRespon.State = StatusCodes.Status204NoContent;
                        genericRespon.Message = "No se Encontraron Resultados";
                        return genericRespon;
                    }

                    genericRespon.State = StatusCodes.Status200OK;
                    genericRespon.Message = "Consultado Correctamente";
                    genericRespon.Data = temp;
                    return genericRespon;
                });

                return task;
            }
            catch (Exception)
            {
                genericRespon.State = StatusCodes.Status503ServiceUnavailable;
                genericRespon.Message = "Error de Servicios";
                return genericRespon;
            }
        }

        public async Task<GenericRespon> GetByPage(GenericPaginatorRequest<string> paginator)
        {
            GenericRespon genericRespon = new GenericRespon();
            try
            {
                var task = await Task<GenericRespon>.Factory.StartNew(() => {

                    _command.Connection = _contextDB.OpenConection();
                    _command.CommandText = "SelectFullUsers";
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.Parameters.AddWithValue("@page", paginator.Page);
                    _command.Parameters.AddWithValue("@length", paginator.RecordsByPage);
                    _read = _command.ExecuteReader();


                    var model = new List<UserModel>();
                    while (_read.Read())
                    {
                        var temp = new UserModel()
                        {
                            Id = _read.GetGuid(0),
                            Acount = _read.GetString(3),
                            Role = _read.GetString(1),
                            RoleId = _read.GetInt32(2),
                            Curp = _read.GetString(4),
                            LastName = _read.GetString(5),
                            SecondLastName = _read.GetString(6),
                            Name = _read.GetString(7),
                            Salary = _read.GetDouble(8),
                            Phone = _read.GetString(9),

                        };

                        model.Add(temp);
                    }
                    _command.Parameters.Clear();
                    _command.Connection = _contextDB.CloseConection();

                    GenericPagiantorRespon<UserModel> paginatorRespon = new GenericPagiantorRespon<UserModel>();

                    if (model.Count <= 0)
                    {
                        genericRespon.State = StatusCodes.Status204NoContent;
                        genericRespon.Message = "No se Encontraron Resultados";
                        paginatorRespon.TotalRecords = 0;
                        paginatorRespon.RecordsByPage = 10;
                        paginatorRespon.TotalPages = 0;
                        genericRespon.Data = paginatorRespon;
                        return genericRespon;
                    }

                    paginatorRespon.TotalRecords = CountUsers();
                    paginatorRespon.RecordsByPage = paginator.RecordsByPage;
                    paginatorRespon.TotalPages = paginatorRespon.TotalPages/paginatorRespon.RecordsByPage;
                    paginatorRespon.Data = model;

                    genericRespon.State = StatusCodes.Status200OK;
                    genericRespon.Message = "Consultado Correctamente";
                    genericRespon.Data = paginatorRespon;
                    return genericRespon;
                });

                return task;
            }
            catch (Exception)
            {
                genericRespon.State = StatusCodes.Status503ServiceUnavailable;
                genericRespon.Message = "Error de Servicios";
                return genericRespon;
            }
        }

        public Task<GenericRespon> GetFull()
        {
            throw new NotImplementedException();
        }

        public async Task<GenericRespon> Update(UpdateUserRequest model)
        {
            GenericRespon genericRespon = new GenericRespon();
            try
            {

                var task = await Task<GenericRespon>.Factory.StartNew(() =>
                {
                    var exits = ExistUser(model.Id);

                    if (!exits)
                    {
                        genericRespon.State = StatusCodes.Status406NotAcceptable;
                        genericRespon.Message = "Registro inexistente";
                        return genericRespon;
                    }


                    var id = Guid.NewGuid();
                    _command.Connection = _contextDB.OpenConection();
                    _command.CommandText = "UpdateUser";
                    _command.CommandType = CommandType.StoredProcedure;

                    _command.Parameters.AddWithValue("@idUser", model.Id);
                    _command.Parameters.AddWithValue("@curp", model.Curp);
                    _command.Parameters.AddWithValue("@name", model.Name);
                    _command.Parameters.AddWithValue("@lastName", model.LastName);
                    _command.Parameters.AddWithValue("@secondLastName", model.SecondLastName);
                    _command.Parameters.AddWithValue("@salary", model.Salary);
                    _command.Parameters.AddWithValue("@phone", model.Phone);

                    _command.ExecuteNonQuery();
                    _command.Parameters.Clear();
                    _command.Connection = _contextDB.CloseConection();

                    genericRespon.State = StatusCodes.Status200OK;
                    genericRespon.Message = "Agregado correctamente";
                    genericRespon.Data = id;
                    return genericRespon;
                });

                return task;
            }
            catch (Exception)
            {
                genericRespon.State = StatusCodes.Status503ServiceUnavailable;
                genericRespon.Message = "Error de Servicios";
                return genericRespon;
            }
        }

        private int CountUsers()
        {
            _command.Connection = _contextDB.OpenConection();
            _command.CommandText = "countUsers";
            _command.CommandType = CommandType.StoredProcedure;
            _read = _command.ExecuteReader();

            int count = 0;
            while (_read.Read())
            {
                count = _read.GetInt32(0);
            }
            _command.Parameters.Clear();
            _command.Connection = _contextDB.CloseConection();
            return count;
        }

        private bool ExistUser(Guid id)
        {
            _command.Connection = _contextDB.OpenConection();
            _command.CommandText = "exitsUser";
            _command.CommandType = CommandType.StoredProcedure;
            _command.Parameters.AddWithValue("@id", id);
            _read = _command.ExecuteReader();

            bool exist = false;
            while (_read.Read())
            {
                exist = _read.GetBoolean(0);
            }
            _command.Parameters.Clear();
            _command.Connection = _contextDB.CloseConection();
            return exist;

        }

        private bool ExistCurp(string curp)
        {
            _command.Connection = _contextDB.OpenConection();
            _command.CommandText = "exitsCurpUser";
            _command.CommandType = CommandType.StoredProcedure;
            _command.Parameters.AddWithValue("@curp", curp);
            _read = _command.ExecuteReader();

            bool exist = false;
            while (_read.Read())
            {
                exist = _read.GetBoolean(0);
            }
            _command.Parameters.Clear();
            _command.Connection = _contextDB.CloseConection();
            return exist;

        }
    }
}
