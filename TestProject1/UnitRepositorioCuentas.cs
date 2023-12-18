using ManejoPresupuesto.Models;
using ManejoPresupuesto.Servicios;
using Moq;
using System.Data;

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

        [Fact]
        public async Task PruebaBuscar()
        {
            //Arrange
            var servicio = new RepositorioCuentas();
            var cuenta = new Cuenta() { Id=1 };

            //Act
            Task resultado = servicio.Buscar(cuenta.TipoCuentaId);

            //Assert
            Assert.True(resultado.IsCompleted);
        }

        [Fact]
        public async Task PruebaObtenerPorId()
        {
            //Arrange
            var servicio = new RepositorioCuentas();
            var cuenta = new Cuenta() { Id = 1 };

            //Act
            Task resultado = servicio.ObtenerPorId(cuenta.Id,cuenta.TipoCuentaId);

            //Assert
            Assert.True(resultado.IsCompleted);
        }

        [Fact]
        public async Task PruebaActualizar()
        {
            //Arrange
            var servicio = new RepositorioCuentas();
            var viewCuenta = new CuentaCreacionViewModel { Id = 1 , TipoCuentaId=1,Nombre=string.Empty};

            //Act
            Task resultado = servicio.Actualizar(viewCuenta);

            //Assert
            Assert.True(resultado.IsCompleted);
        }

        [Fact]
        public async Task PruebaBorrar()
        {
            //Arrange
            var servicio = new RepositorioCuentas();
            var cuenta = new Cuenta() { Id=0 };

            //Act
            Task resultado = servicio.Borrar(cuenta.Id);

            //Assert
            Assert.True(resultado.IsCompleted);
        }

        [Fact]
        public void CrearCuentaDebeDevolverUnValorNoNulo()
        {
            var servicio = new RepositorioCuentas();
            // Crea una cuenta
            Cuenta cuenta = new Cuenta() { Nombre = "Cuenta 1", TipoCuentaId = -1, Descripcion = "Descripción de la cuenta 1", Balance = 100 };

            // Llama al método Crear()
            var id = servicio.Crear(cuenta);

            // Verifica que el método Crear() haya devuelto un valor no nulo
            Assert.NotNull(id);
        }
    }
}