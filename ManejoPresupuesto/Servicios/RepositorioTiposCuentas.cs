using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Servicios
{
    /*Creacion de la interfaz*/
    public interface IRepositorioTiposCuentas
    {
        Task Crear(TipoCuenta tipoCuenta);
        Task<bool> Existe(string nombre, int usuarioId);
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
        public async Task Crear(TipoCuenta tipoCuenta)
        {
            /*Nueva instancia de base de datos*/
            using var connection = new SqlConnection(connectionString);
            /*Hace el insert segun el ususario id*/
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO TiposCuentas(Nombre,UsuarioId,Orden) VALUES (@Nombre, @UsuarioId,0); SELECT SCOPE_IDENTITY();", tipoCuenta);
            /*Devuelve ese id y lo almacena en el modelo de Id*/
            tipoCuenta.Id = id;
        }
        /*Validacion para comprobar que ya existe dicho registro y que no se duplique*/
        /*Hace que sea una tarea asincrona y manda el nombre y usuario id porque necesita comprobar el mensaje por id*/
        /*Es bool porque asi comprueba que solo exista, no se necesita el contenido del llamado*/
        public async Task<bool> Existe(string nombre, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            /*con QueryFirstOrDefaultAsync compuruba que exista un solo registro o si no existe como trae un int pues el default seria 0 lo cual significa que no existe*/
            var existe = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM TiposCuentas WHERE Nombre=@Nombre AND UsuarioId=@UsuarioId", new {nombre,usuarioId});
            return existe==1;
        }
    }
}
