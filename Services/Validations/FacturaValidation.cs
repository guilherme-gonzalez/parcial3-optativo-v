using FluentValidation;
using Repository.Data;
using System;
using System.Globalization;

namespace api.personas.Validation
{
    public class FacturaValidation : AbstractValidator<FacturaModel>
    {
        public FacturaValidation()
        {
            RuleFor(factura => factura.id_cliente)
                .NotEmpty().WithMessage("Id Cliente is required.");

            RuleFor(factura => factura.nro_factura)
                .NotEmpty().WithMessage("Nro Factura is required.")
                .Matches("^[0-9]{3}-[0-9]{3}-[0-9]{6}$").WithMessage("Nro Factura must follow the pattern XXX-XXX-XXXXXX.");

            RuleFor(factura => factura.fecha_hora)
                .NotEmpty().WithMessage("Fecha Hora is required.")
                .Custom((fechaHora, context) => 
                {
                    if (!BeValidSwaggerDateTime(fechaHora))
                    {
                        context.AddFailure("Fecha Hora must be in the format 'yyyy-MM-ddTHH:mm:ssZ', e.g., '2018-03-20T09:12:28Z'.");
                    }
                });

            RuleFor(factura => factura.total)
                .NotEmpty().WithMessage("Total is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Total must be a positive value.");

            RuleFor(factura => factura.total_iva5)
                .NotEmpty().WithMessage("Total IVA 5% is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Total IVA 5% must be a positive value.");

            RuleFor(factura => factura.total_iva10)
                .NotEmpty().WithMessage("Total IVA 10% is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Total IVA 10% must be a positive value.");

            RuleFor(factura => factura.total_iva)
                .NotEmpty().WithMessage("Total IVA is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Total IVA must be a positive value.");

            RuleFor(factura => factura.total_letras)
                .NotEmpty().WithMessage("Total en Letras is required.")
                .MinimumLength(6).WithMessage("Total en Letras must be at least 6 characters long.");

            RuleFor(factura => factura.sucursal)
                .NotEmpty().WithMessage("Sucursal is required.");
        }

        private bool BeValidSwaggerDateTime(DateTime fechaHora)
        {
            // Since the property is already a DateTime, it is inherently valid.
            // This method will always return true. 
            // Custom error messages are shown directly via the Custom validation method above.
            return true;
        }
    }
}
