using ApiTemplate.Services.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace ApiTemplate.Services
{
    public class ExampleService : IExampleService
    {
        private readonly IDbContextService _contextDB;
        private readonly IWebHostEnvironment _env;
        private readonly SqlDataReader? _read;
        private readonly SqlCommand _command;

        public ExampleService(IDbContextService contextDB, IWebHostEnvironment env)
        {
            _contextDB = contextDB;
            _env = env;
            _command = new SqlCommand();
        }

        public Task<object> AcctionExample(object? parameters)
        {
            /*var task = await Task<object>.Factory.StartNew(() =>
            {
                var id = Guid.NewGuid();
                _command.Connection = _contextDB.OpenConection();
                _command.CommandText = "InsertProspecto";
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.AddWithValue("@ParameterName", parameters);

                _command.ExecuteNonQuery();
                _command.Parameters.Clear();
                _command.Connection = _contextDB.CloseConection();

                return id;
            });

            return task;*/
            throw new NotImplementedException();
        }

        public Task<object> SelectExample(object? parameters)
        {
            /*var task = await Task<List<object>>.Factory.StartNew(() => {
                _command.Connection = _contextDB.OpenConection();
                _command.CommandText = "GetDocumentsByProspecto";
                _command.CommandType = CommandType.StoredProcedure;
                _command.Parameters.AddWithValue("@ParameterName", parameters);
                _read = _command.ExecuteReader();


                var model = new List<object>();
                while (_read.Read())
                {
                    var temp = new object()
                    {
                        Id = _read.GetString(0),
                        IdProspecto = _read.GetString(1),
                        NameDocument = _read.GetString(2),
                        Link = _read.GetString(3)
                    };

                    model.Add(temp);
                }
                _command.Parameters.Clear();
                _command.Connection = _contextDB.CloseConection();
                return model;
            });

            return task;*/
            throw new NotImplementedException();
        }
    }
}
