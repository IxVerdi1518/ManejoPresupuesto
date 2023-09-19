using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuesto.Validaciones
{
    public class PrimeraLetraMayusculaAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            /*Verifica si el campo value en este caso que recibe el parametro del modelo de donde se lo llama en el modelo es nulo*/
            if(value == null|| string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }
            /*Toma la letra de la palabra que venga*/
            var primeraLetra = value.ToString()[0].ToString();
            /*Verifica si esta es distinta de la mayuscula*/
            if(primeraLetra != primeraLetra.ToUpper())
            {
                /*Si no cumple manda el mensaje de error*/
                return new ValidationResult("La primera letra debe de ser mayuscula");
            }
            /*Sino todo esta en orden*/
            return ValidationResult.Success;
        }
    }
}
