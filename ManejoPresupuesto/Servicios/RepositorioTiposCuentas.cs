using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Servicios
{
    /*Creacion de la interfaz*/
    public interface IRepositorioTiposCuentas
    {
        void Crear(TipoCuenta tipoCuenta);
    }
    public class RepositorioTiposCuentas: IRepositorioTiposCuentas
    {
        /*Clase que hereda de la interfaz para ocupar*/
        private readonly string connectionString;
        public RepositorioTiposCuentas(IConfiguration configuration)
        {
            /*Clase constructora para poder llamar desde appsetings la base de datos*/
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public void Crear(TipoCuenta tipoCuenta)
        {
            /*Nueva instancia de base de datos*/
            using var connection = new SqlConnection(connectionString);
            /*Hace el insert segun el ususario id*/
            var id = connection.QuerySingle<int>(@"INSERT INTO TiposCuentas(Nombre,UsuarioId,Orden) VALUES (@Nombre, @UsuarioId,0); SELECT SCOPE_IDENTITY();", tipoCuenta);
            /*Devuelve ese id y lo almacena en el modelo de Id*/
            tipoCuenta.Id = id;
        }
    }
}
