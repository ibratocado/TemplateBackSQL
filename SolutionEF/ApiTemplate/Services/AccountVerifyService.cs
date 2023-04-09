using ApiTemplate.DTO.Request;
using ApiTemplate.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Security;
using System.Security.Claims;
using System.Text;

namespace ApiTemplate.Services
{
    public class AccountVerifyService : IAccountVerifyService
    {
        private readonly SecureString? _verify;

        public AccountVerifyService(IConfiguration configuration)
        {
            //Se trae la key para la creacion del claim de jwt
            _verify = new SecureString();
            var arg = configuration.GetSection("settings").GetSection("key").ToString().ToArray();
            //Despues se convierte la key en un secure string
            Array.ForEach( arg, _verify.AppendChar);
        }

        public async Task<object> GetValidate(RequestAccount data)
        {
            //Checamos que la key no venga vacia
            if (_verify != null)
                return Task<object>.Factory.StartNew(() => { return "Error de servicios"; });

            //Traemos los datos del usuario y cuenta 
            var kiss = _verify.Copy().ToString();
            var autent = ""; //await _context.Accounts.FirstOrDefaultAsync(i => i.Count == data.account && i.Pount == data.pount);
            var acount = ""; //await _context.Clients.FirstOrDefaultAsync(i => i.AccountId == aut.Id);

            if (autent == null || acount == null)
                return Task<object>.Factory.StartNew(()=> { return "User No Content"; });

            List<object> claims = new List<object>();

            return CreateBearer(claims,kiss);

            
        }

        private string CreateBearer(List<object> data, string kiss)
        {
            try
            {
                //Transformamos la key en un arreglo de bytes y creamos el claim
                var bytes = Encoding.ASCII.GetBytes(kiss);
                var claim = new ClaimsIdentity();

                //Asignamos los cleims que se regresaran 
                data.ForEach(item =>
                {
                    claim.AddClaim(new Claim("data[0].parameter", "data[0].data"));
                });


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
