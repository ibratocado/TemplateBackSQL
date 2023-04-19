using ApiTemplate.DTO.Request;
using ApiTemplate.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Security;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Services
{
    public class AccountVerifyService : IAccountVerifyService
    {
        private readonly SecureString? _verify;
        private readonly Db_TemplateContext _templateContext;

        public AccountVerifyService(IConfiguration configuration, Db_TemplateContext templateContext)
        {
            _templateContext = templateContext;
            //Se trae la key para la creacion del claim de jwt
            _verify = new SecureString();
            var arg = configuration.GetSection("settings").GetSection("key").ToString().ToArray();
            //Despues se convierte la key en un secure string
            Array.ForEach( arg, _verify.AppendChar);
        }

        public async Task<object> GetValidate(AccountRequest data)
        {
            var anyExist = await _templateContext.Accounts.AnyAsync(i => i.Acount == data.account && data.pount.Equals(i.Pount));
            if (anyExist)
                return Task<object>.Factory.StartNew(() => { return "Usuario No Encontrado"; });

            //Checamos que la key no venga vacia
            if (_verify != null)
                return Task<object>.Factory.StartNew(() => { return "Error de servicios"; });

            //Traemos los datos del usuario y cuenta 
            var autent = await _templateContext.Accounts.
                                    Include(i=> i.Role).
                                    FirstOrDefaultAsync(i => i.Acount == data.account && data.pount.Equals(i.Pount));
            //var acount = ""; //await _context.Clients.FirstOrDefaultAsync(i => i.AccountId == aut.Id);

            return  await Task<object>.Factory.StartNew(()=> { return CreateBearer(autent); });
            
        }

        private string CreateBearer(Account data)
        {
            try
            {
                var kiss = _verify.Copy().ToString();
                //Transformamos la key en un arreglo de bytes y creamos el claim
                var bytes = Encoding.ASCII.GetBytes(kiss);
                var claim = new ClaimsIdentity();

                //Asignamos los cleims que se regresaran 
                claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, data.Id.ToString()));
                claim.AddClaim(new Claim("Role", data.RoleId.ToString()));
                claim.AddClaim(new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(10).ToString()));


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
