﻿using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuesto.Models
{
    public class Transaccion
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaTransaccion { get; set; } = DateTime.Today;
        public decimal Monto { get; set; }
        [Range(1, int.MaxValue,ErrorMessage ="Debe seleccionar una categoria")]
        public int CategoriaId { get; set; }
        [StringLength(maximumLength:1000, ErrorMessage ="La nota no puede pasar de caracteres")]
        public string Nota { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoria")]
        public int CuentaId { get; set; }
    }
}
