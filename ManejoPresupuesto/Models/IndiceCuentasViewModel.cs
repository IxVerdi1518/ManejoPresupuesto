namespace ManejoPresupuesto.Models
{
    public class IndiceCuentasViewModel
    {
        /*Misma funcionalidad, que la otra viewmodel para poder ir ocupando y extendiendo los datos*/
        public string TipoCuenta { get; set; }
        public IEnumerable<Cuenta> Cuentas { get; set; }
        public decimal Balance => Cuentas.Sum(c => c.Balance);
    }
}
