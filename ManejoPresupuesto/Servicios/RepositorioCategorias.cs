using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Servicios
{
    public interface IRepositorioCategorias
    {
        Task Actualizar(Categoria categoria);
        Task Borrar(int id);
        Task Crear(Categoria categoria);
        Task<IEnumerable<Categoria>> Obtener(int usuarioId);
        Task<Categoria> ObtenerPorId(int id, int usuarioId);
    }
    public class RepositorioCategorias:IRepositorioCategorias
    {
        private readonly string connectioString;
        public RepositorioCategorias(IConfiguration configuration)
        {
            connectioString = configuration.GetConnectionString("DefaultConnection");
        }

        public RepositorioCategorias()
        {
        }

        public async Task Crear(Categoria categoria)
        {
            using var connection = new SqlConnection(connectioString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO Categorias(Nombre, TipoOperacionId, UsuarioId) VALUES (@Nombre,@TipoOperacionId,@UsuarioId); SELECT SCOPE_IDENTITY()", categoria);
            categoria.Id= id;
        }

        public async Task<IEnumerable<Categoria>> Obtener(int usuarioId)
        {
            using var connection= new SqlConnection(connectioString);
            return await connection.QueryAsync<Categoria>("Select * from Categorias WHERE UsuarioId = @UsuarioId",new {usuarioId});
        }

        public async Task<Categoria> ObtenerPorId(int id, int usuarioId)
        {
            using var connection = new SqlConnection(connectioString);
            return await connection.QueryFirstOrDefaultAsync<Categoria>(@"Select * from Categorias where Id=@Id and UsuarioId=@UsuarioId", new {id,usuarioId});
        }
        public async Task Actualizar(Categoria categoria)
        {
            using var connection = new SqlConnection(connectioString);
            await connection.ExecuteAsync(@"Update Categorias set Nombre=@Nombre, TipoOperacionId=@TipoOperacionId Where Id= @Id", categoria);
        }
        public async Task Borrar (int id)
        {
            using var connection = new SqlConnection(connectioString);
            await connection.ExecuteAsync(@"Delete Categorias WHERE Id=@Id",new {id});
        }
    }
}
