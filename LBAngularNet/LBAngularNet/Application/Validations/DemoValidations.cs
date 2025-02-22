using FluentValidation;
using LBAngularNet.Core.Domain.Entities;

namespace LBAngularNet.Application.Validations
{
    public class DemoValidations : AbstractValidator<Demo>
    {
        public DemoValidations() 
        {
            RuleFor(u => u.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MinimumLength(3).WithMessage("El nombre debe tener minimo 3 caracteres")
                .WithSeverity(Severity.Error);

            RuleFor(u => u.Id)
                .LessThan(0).WithMessage("El Id es obligatorio que sea mayor a 0 ")
                .WithSeverity(Severity.Error);


        }
    }
}
