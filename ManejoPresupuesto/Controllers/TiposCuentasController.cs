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
        private readonly IServiciosUsuarios serviciosUsuarios;

        /*Llama para la interfaz previa configutacion del servicio en el program*/
        public TiposCuentasController(IRepositorioTiposCuentas repositorioTiposCuentas, IServiciosUsuarios serviciosUsuarios)
        {
            this.repositorioTiposCuentas = repositorioTiposCuentas;
            this.serviciosUsuarios = serviciosUsuarios;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();
            var tiposCuentas = await repositorioTiposCuentas.Obtener(usuarioId);
            return View(tiposCuentas);
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
            tipoCuenta.UsuarioId = serviciosUsuarios.ObtenerUsuarioId();
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
            /*Una vez hecho todo, redirecciona al index*/
            return RedirectToAction("Index");
        }

        /*Metodo para poder verificar que es la persona correcta que quiere editar*/
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();
            var tipoCuenta = await repositorioTiposCuentas.ObtenerPorId(id, usuarioId);
            /*Retorna un verdadero si o si, sino sale error, o sino no existe y no encuentra*/
            if (tipoCuenta is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            /*Sino redirecciona a la venta para editar*/
            return View(tipoCuenta);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(TipoCuenta tipoCuenta)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();
            var tipoCuentaExiste = await repositorioTiposCuentas.ObtenerPorId(tipoCuenta.Id, usuarioId);
            /*Comprueba nuevamente*/
            if (tipoCuentaExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            /*Si retorna verdadero si o si, actualiza los campos*/
            await repositorioTiposCuentas.Actualizar(tipoCuenta);
            /*Y devuelve a la venta de Index*/
            return RedirectToAction("Index");
        }

        /*Metodo para enviar a la vista de borrar sigueidno la misma logica de editar*/
        [HttpGet]
        public async Task<IActionResult> Borrar(int id)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();
            var tipoCuenta = await repositorioTiposCuentas.ObtenerPorId(id, usuarioId);
            if(tipoCuenta is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            return View(tipoCuenta);
        }

        /*Metodo para realziar el borrado*/
        [HttpPost]
        public async Task<IActionResult> BorrarTipoCuenta(int id)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();
            var tipoCuenta = await repositorioTiposCuentas.ObtenerPorId(id, usuarioId);
            if (tipoCuenta is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            await repositorioTiposCuentas.Borrar(id);
            return RedirectToAction("Index");
        }

        /*Para poder validar sin necesidad de enviar el formulario se tiene que hacer una peticion al servidor de tipo get*/
        [HttpGet]
        public async Task<IActionResult> VerificarExisteTipoCuenta(string nombre)
        {
            /*Con el mismo metodo que ya se creo para verificar la existencia*/
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();
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
