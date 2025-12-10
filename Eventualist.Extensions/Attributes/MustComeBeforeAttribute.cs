using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Eventualist.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MustComeBeforeAttribute(string otherProperty)
        : ValidationAttribute($"Must occur before {otherProperty}")
    {
        public string OtherProperty { get; } = otherProperty ?? throw new ArgumentNullException(nameof(otherProperty));

        public override bool RequiresValidationContext => true;

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {

            if (value is null)
            {
                return ValidationResult.Success!;
            }


            PropertyInfo? otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);
            if (otherPropertyInfo is null)
            {
                return new ValidationResult($"Unknown property: {OtherProperty}");
            }

            if (!typeof(DateTime).IsAssignableFrom(otherPropertyInfo.PropertyType))
            {
                return new ValidationResult($"Property {OtherProperty} is not a DateTime type");
            }

            if (value is not DateTime currentDateTime)
            {
                return new ValidationResult("Current property is not a DateTime type");
            }

            object? otherValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance);
            if (otherValue is null)
            {
                return ValidationResult.Success!; // If other value is null, we can't compare, so it's valid
            }

            DateTime otherDateTime = (DateTime)otherValue;
            
            return currentDateTime < otherDateTime
                ? ValidationResult.Success!
                : new ValidationResult($"This date must occur before {OtherProperty}");
        }
    }
}
