using ApiTemplate.DTO.Respon;
using ApiTemplate.Services.Interfaces;
using ModelsDB;
using System.Data;
using System.Data.SqlClient;

namespace ApiTemplate.Services
{
    public class RolsService : IRolsService
    {
        private readonly IDbContextService _contextDB;
        private SqlDataReader? _read;
        private readonly SqlCommand _command;

        public RolsService(IDbContextService contextDB)
        {
            _contextDB = contextDB;
            _command = new SqlCommand();
        }

        public async Task<GenericRespon> GetFullRols()
        {
            GenericRespon genericRespon = new GenericRespon();
            try
            {
                var task = await Task<GenericRespon>.Factory.StartNew(() => {

                    _command.Connection = _contextDB.OpenConection();
                    _command.CommandText = "selectRols";
                    _command.CommandType = CommandType.StoredProcedure;
                    _read = _command.ExecuteReader();

                    List<RolsModel> list = new List<RolsModel>();
                    while (_read.Read())
                    {
                        var temp = new RolsModel()
                        {
                            Id = _read.GetInt32(0),
                            Name = _read.GetString(1),

                        };

                        list.Add(temp);

                    }
                    _command.Parameters.Clear();
                    _command.Connection = _contextDB.CloseConection();

                    GenericPagiantorRespon<UserModel> paginatorRespon = new GenericPagiantorRespon<UserModel>();

                    if (list.Count <= 0)
                    {
                        genericRespon.State = StatusCodes.Status204NoContent;
                        genericRespon.Message = "No se Encontraron Resultados";
                        return genericRespon;
                    }

                    genericRespon.State = StatusCodes.Status200OK;
                    genericRespon.Message = "Consultado Correctamente";
                    genericRespon.Data = list;
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
    }
}
