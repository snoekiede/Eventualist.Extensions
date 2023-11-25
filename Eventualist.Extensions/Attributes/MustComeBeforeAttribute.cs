using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eventualist.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = false)]
    public class MustComeBeforeAttribute:ValidationAttribute
    {
        public string OtherProperty { get; private set; }


        public MustComeBeforeAttribute(string otherProperty) : base("Must match")
        {
            if (string.IsNullOrEmpty(otherProperty))
            {
                throw new ArgumentNullException("otherProperty");
            }

            this.OtherProperty=otherProperty;
        }

        public override bool RequiresValidationContext
        {
            get
            {
                return true;
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo otherPropertyInfo = validationContext.ObjectType.GetProperty(this.OtherProperty);

            if (otherPropertyInfo == null)
            {
                return new ValidationResult($"Unknown property: {this.OtherProperty}");
            }

            try
            {
                DateTime otherPropertyValue =
                    (DateTime)otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);
                if (otherPropertyValue == null)
                {
                    return new ValidationResult($"{this.OtherProperty} not of type DateTime");
                }

                var currentDateTime = (DateTime)value;
                if (currentDateTime < otherPropertyValue)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Dates are in reverse");
                }
            }
            catch (Exception e)
            {
                return new ValidationResult($"Exception: {e.Message}");
            }

        }

    }
}
