using ApiTemplate.DTO.Request;
using ApiTemplate.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ModelsDB;
using ApiTemplate.DTO.Respon;
using System.IdentityModel.Tokens.Jwt;

namespace ApiTemplate.Services
{
    public class AccountVerifyService : IAccountVerifyService
    {
        private readonly string _verify;
        private readonly IDbContextService _contextDB;
        private SqlDataReader? _read;
        private readonly SqlCommand _command;

        public AccountVerifyService(IConfiguration configuration, IDbContextService contextDB)
        {
            //Se trae la key para la creacion del claim de jwt
            _verify = configuration.GetSection("settings").GetSection("secretKey").ToString();

            _contextDB = contextDB;
            _command = new SqlCommand();
        }

        public async Task<GenericRespon> GetValidate(AccountRequest data)
        {

            var respon = new GenericRespon();
            var exits = await GetExitsAccount(data);
            if (!exits)
                return await Task<GenericRespon>.Factory.StartNew(() =>
                {
                    respon.State = 203;
                    respon.Message = "User No Content";
                    return respon;
                });

            //Checamos que la key no venga vacia
            if (_verify.Length <= 0)
                return await Task<GenericRespon>.Factory.StartNew(() =>
                {
                    respon.State = 503;
                    respon.Message = "Service Error";
                    return respon;
                });

            //Traemos los datos del usuario y cuenta 

            var autent = await GetAccountVerify(data); //await _context.Accounts.FirstOrDefaultAsync(i => i.Count == data.account && i.Pount == data.pount);
            //var acount = ""; //await _context.Clients.FirstOrDefaultAsync(i => i.AccountId == aut.Id);


            return await Task<GenericRespon>.Factory.StartNew(() => 
            { 
                respon.State = 200; 
                respon.Message = "Consultado Correctamente"; 
                respon.Data = CreateBearer(autent); 
                return respon;
            });

            
        }

        private async Task<bool> GetExitsAccount(AccountRequest data)
        {
            return await Task<bool>.Factory.StartNew(() =>
            {
                _command.Connection = _contextDB.OpenConection();
                _command.CommandText = "existAccount";
                _command.CommandType = CommandType.StoredProcedure;
                _command.Parameters.AddWithValue("@count", data.account);
                _command.Parameters.AddWithValue("@pount", data.pount);
                _read = _command.ExecuteReader();

                bool success = false;
                while(_read.Read())
                {
                    success = _read.GetBoolean(0);
                }
                _command.Parameters.Clear();
                _command.Connection = _contextDB.CloseConection();

                return success;
            });
        }

        private async Task<AccountModel> GetAccountVerify(AccountRequest data)
        {
            return await Task<AccountModel>.Factory.StartNew(() =>
            {
                _command.Connection = _contextDB.OpenConection();
                _command.CommandText = "accountVerify";
                _command.CommandType = CommandType.StoredProcedure;
                _command.Parameters.AddWithValue("@count", data.account);
                _command.Parameters.AddWithValue("@pount", data.pount);
                _read = _command.ExecuteReader();

               var model = new AccountModel();
                while (_read.Read())
                {
                    model.Id = _read.GetGuid(0);
                    model.RoleId = _read.GetInt32(1);
                }
                _command.Parameters.Clear();
                _command.Connection = _contextDB.CloseConection();

                return model;
            });
        }

        private string CreateBearer(AccountModel data)
        {
            try
            {
                string kiss = _verify;
                //Transformamos la key en un arreglo de bytes y creamos el claim
                var bytes = Encoding.ASCII.GetBytes(kiss);
                var claim = new ClaimsIdentity();

                //Asignamos los cleims que se regresaran 
                claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, data.Id.ToString()));
                claim.AddClaim(new Claim("Role", data.RoleId.ToString()));


                //Creamos la descripcion del token, ponemos duracion y su algoritmo de codificaicon
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claim,
                    Expires = DateTime.UtcNow.AddDays(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(bytes), SecurityAlgorithms.HmacSha256Signature)
                };

                //por ultimo creamos el token final con la configuracion antes ya hecha
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenconf = tokenHandler.CreateToken(tokenDescriptor);
                string tokenCreate = tokenHandler.WriteToken(tokenconf);

                return tokenCreate;
            }
            catch (Exception)
            {
                return "Error Create Autentication";
            }
                
        }
    }
}
