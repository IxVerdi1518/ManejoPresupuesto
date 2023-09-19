using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Controllers
{
    public class TiposCuentasController: Controller
    {
        private readonly string connectionString;
        public TiposCuentasController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public IActionResult Crear()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = connection.Query("Select 1").FirstOrDefault();
            }
            return View();
        }
        [HttpPost]
        /*El IAction devuleve cualquier tipo de resultado como json,etc*/
        public IActionResult Crear(TipoCuenta tipoCuenta)
        {
            /*Valida lo que venga los requerimientos del modelo*/
            if(!ModelState.IsValid)
            {
                return View(tipoCuenta);
            }
            return View();
        }
    }
}
