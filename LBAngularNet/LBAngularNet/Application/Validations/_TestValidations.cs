using FluentValidation.Results;
using LBAngularNet.Core.Domain.Entities;

namespace LBAngularNet.Application.Validations
{
    public class _TestValidations
    {
        public void Test() 
        {
            var demo = new Demo
            {
                Id = 1,
                Name = "TestName"
            };

            var validation = new DemoValidations();

            ValidationResult _result = validation.Validate(demo);

            if (!_result.IsValid) 
            {
                Console.WriteLine($"Error en validator");
            }
            else
            {
                Console.WriteLine($"Objeto Ok");
            }
        }   
    }
}
