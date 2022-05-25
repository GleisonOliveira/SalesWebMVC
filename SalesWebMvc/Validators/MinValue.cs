using Microsoft.Extensions.Localization;
using SalesWebMvc.Models;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Validators
{
    public class MinValue: ValidationAttribute
    {
        private readonly double _minValue;
        public MinValue(double minValue)
        {
            _minValue = minValue;
            ErrorMessage = "The min value is {0}";
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var _localizationService = (IStringLocalizer)validationContext.GetService(typeof(IStringLocalizer<Shared>));

            if (value == null)
            {
                return null;
            }

            if ((double)value < _minValue)
            {
                return new ValidationResult(_localizationService != null ? _localizationService.GetString(ErrorMessage, _minValue) : ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
