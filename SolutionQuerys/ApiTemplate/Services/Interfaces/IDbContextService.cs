using System.Data.SqlClient;

namespace ApiTemplate.Services.Interfaces
{
    public interface IDbContextService
    {
        SqlConnection OpenConection();
        SqlConnection CloseConection();
    }
}
