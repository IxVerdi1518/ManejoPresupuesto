using ManejoPresupuesto.Models;
using ManejoPresupuesto.Servicios;

namespace TestProject1
{
    public class UnitRepositorioCuentas
    {
       
        [Fact]
        public async Task PruebaCrearCuenta()
        {
            //Arrange
            var servicio = new RepositorioCuentas();
            var cuenta = new Cuenta() { Nombre=string.Empty };

            //Act
            Task resultado = servicio.Crear(cuenta);

            //Assert
            Assert.True(resultado.IsCompleted);
        }
    }
}