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
        public async Task<IActionResult> Crear(TipoCuenta tipoCuenta)
        {
            /*Valida lo que venga los requerimientos del modelo*/
            if(!ModelState.IsValid)
            {
                return View(tipoCuenta);
            }
            /*Quema el ususairo id de momento*/
            tipoCuenta.UsuarioId = 1;
            /*Crea una variable para ocupar la clase que comprueba si ya existe ese registro y le manda las varables necesarias*/
            var yaExisteTipoCuenta = await repositorioTiposCuentas.Existe(tipoCuenta.Nombre, tipoCuenta.UsuarioId);
            if (yaExisteTipoCuenta)
            {
                /*Si esto es verdadero lo que significa que devuelve un 1 desde la clase, es porque ya existe*/
                ModelState.AddModelError(nameof(tipoCuenta.Nombre), $"El nombre {tipoCuenta.Nombre} ya existe");
                /*Y retorna a la misma vista pero con el campo que envio*/
                return View(tipoCuenta);
            }
            /*Ocupa el metodo de crear en base al modelo*/
            await repositorioTiposCuentas.Crear(tipoCuenta);
            return View();
        }
        /*Para poder validar sin necesidad de enviar el formulario se tiene que hacer una peticion al servidor de tipo get*/
        [HttpGet]
        public async Task<IActionResult> VerificarExisteTipoCuenta(string nombre)
        {
            /*Con el mismo metodo que ya se creo para verificar la existencia*/
            var usuarioId = 1;
            var yaExisteTipoCuenta = await repositorioTiposCuentas.Existe(nombre, usuarioId);
            if (yaExisteTipoCuenta)
            {
                /*Lo que transforma es en json*/
                return Json($"El nombre {nombre} ya existe");
            }
            return Json(true);
        }
    }
}
