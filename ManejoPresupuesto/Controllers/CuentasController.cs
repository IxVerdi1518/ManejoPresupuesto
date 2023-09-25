using ManejoPresupuesto.Models;
using ManejoPresupuesto.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManejoPresupuesto.Controllers
{
    public class CuentasController : Controller
    {
        private readonly IRepositorioTiposCuentas repositorioTiposCuentas;
        private readonly IServiciosUsuarios serviciosUsuarios;

        /*Llama a las interfaces antes hechas para ocupar su contenido*/
        public CuentasController(IRepositorioTiposCuentas repositorioTiposCuentas,IServiciosUsuarios serviciosUsuarios)
        {
            this.repositorioTiposCuentas = repositorioTiposCuentas;
            this.serviciosUsuarios = serviciosUsuarios;
        }

        /*Metodo para mandar a la vista de crear*/
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();
            var tiposCuentas = await repositorioTiposCuentas.Obtener(usuarioId);
            /*Hace el llamado de lo que esta en el modelo para ocuparlo*/
            var modelo = new CuentaCreacionViewModel();
            /*Hace una iteracion donde obtiene el id y el nombre para hacerlo y ocuparlo con el IEnumerable y mostrar en el select, el id es importante aunque no se muestre*/
            modelo.TiposCuentas = tiposCuentas.Select(x => new SelectListItem(x.Nombre, x.Id.ToString()));
            return View(modelo);
        }
    }
}
