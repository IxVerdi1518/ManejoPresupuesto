using Dapper;
using ManejoPresupuesto.Models;
using ManejoPresupuesto.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Controllers
{
    public class TiposCuentasController: Controller
    {
        private readonly IRepositorioTiposCuentas repositorioTiposCuentas;
        /*Llama para la interfaz previa configutacion del servicio en el program*/
        public TiposCuentasController(IRepositorioTiposCuentas repositorioTiposCuentas)
        {
            this.repositorioTiposCuentas = repositorioTiposCuentas;
        }
        public IActionResult Crear()
        {
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
            /*Quema el ususairo id de momento*/
            tipoCuenta.UsuarioId = 1;
            /*Ocupa el metodo de crear en base al modelo*/
            repositorioTiposCuentas.Crear(tipoCuenta);
            return View();
        }
    }
}
