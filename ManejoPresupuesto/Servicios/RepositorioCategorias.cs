using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Servicios
{
    public interface IRepositorioCategorias
    {
        Task Crear(Categoria categoria);
    }
    public class RepositorioCategorias:IRepositorioCategorias
    {
        private readonly string connectioString;
        public RepositorioCategorias(IConfiguration configuration)
        {
            connectioString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(Categoria categoria)
        {
            using var connection = new SqlConnection(connectioString);
            var id = await connection.QuerySingleAsync<int>("@INSERT INTO Categorias(Nombre, TipoOperacionId, UsuarioId) VALUES (@Nombre,@TipoOperacionId,@UsuarioId)", categoria);
            categoria.Id= id;
        }
    }
}
