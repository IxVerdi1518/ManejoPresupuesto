using ManejoPresupuesto.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManejoPresupuesto.Controllers
{
    public class TiposCuentasController: Controller
    {
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
            return View();
        }
    }
}
