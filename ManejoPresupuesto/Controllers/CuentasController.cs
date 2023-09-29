using ManejoPresupuesto.Models;
using ManejoPresupuesto.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace ManejoPresupuesto.Controllers
{
    public class CuentasController : Controller
    {
        private readonly IRepositorioTiposCuentas repositorioTiposCuentas;
        private readonly IServiciosUsuarios serviciosUsuarios;
        private readonly IRepositorioCuentas repositorioCuentas;

        /*Llama a las interfaces antes hechas para ocupar su contenido*/
        public CuentasController(IRepositorioTiposCuentas repositorioTiposCuentas,IServiciosUsuarios serviciosUsuarios, IRepositorioCuentas repositorioCuentas)
        {
            this.repositorioTiposCuentas = repositorioTiposCuentas;
            this.serviciosUsuarios = serviciosUsuarios;
            this.repositorioCuentas = repositorioCuentas;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();
            var cuentasConTipoCuenta = await repositorioCuentas.Buscar(usuarioId);
            /*Aqui llama al metodo que esta en el repo y lo ocupa porque es para listar todo lo que venga segun el tipo de cuenta*/
            var modelo = cuentasConTipoCuenta
                /*Tomando los datos, lo que hace es agrupar por tipos ceunta*/
                .GroupBy(x=> x.TipoCuenta)
                .Select(grupo=>new IndiceCuentasViewModel
                {
                    TipoCuenta = grupo.Key,
                    Cuentas=grupo.AsEnumerable()
                }).ToList();
            return View(modelo);
        }

        /*Metodo para mandar a la vista de crear*/
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();
            
            /*Hace el llamado de lo que esta en el modelo para ocuparlo*/
            var modelo = new CuentaCreacionViewModel();
            /*Hace una iteracion donde obtiene el id y el nombre para hacerlo y ocuparlo con el IEnumerable y mostrar en el select, el id es importante aunque no se muestre*/
            modelo.TiposCuentas = await ObtenerTiposCuentas(usuarioId);
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CuentaCreacionViewModel cuenta)
        {

            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();
            var tipoCuenta = await repositorioTiposCuentas.ObtenerPorId(cuenta.TipoCuentaId, usuarioId);

            if(tipoCuenta is null) { return RedirectToAction("NoEncontrado", "Home"); }
            if (!ModelState.IsValid)
            {
                cuenta.TiposCuentas = await ObtenerTiposCuentas(usuarioId);
                return View(cuenta);
            }
            /*manda todo el contenido del post*/
            await repositorioCuentas.Crear(cuenta);
            return RedirectToAction("Index");
        }

        /*Metodo para hacer el select de los tipos cuentas*/
        private async Task<IEnumerable<SelectListItem>> ObtenerTiposCuentas(int usuarioId)
        {
            var tiposCuentas = await repositorioTiposCuentas.Obtener(usuarioId);
            return tiposCuentas.Select(x => new SelectListItem(x.Nombre, x.Id.ToString()));
        }
    }
}
