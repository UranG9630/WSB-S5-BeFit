using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BeFit.Models
{
    public class IsAfter : ValidationAttribute
    {
        private readonly string _otherProperty;
        public IsAfter(string otherProperty)
        {
            _otherProperty = otherProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var otherProperty = validationContext.ObjectType.GetProperty(_otherProperty);

            if (otherProperty == null)
            {
                return new ValidationResult(string.Format(
                    CultureInfo.CurrentCulture,
                    "Unknown property {0}",
                    new[] { _otherProperty }
                ));
            }
            var objInstance = validationContext.ObjectInstance;
            var otherValue = otherProperty.GetValue(validationContext.ObjectInstance, null);

            if (objInstance is DateTime )
            {
                return new ValidationResult(string.Format(
                    CultureInfo.CurrentCulture,
                    "Property {0} must be type DateTime",
                    new[] { _otherProperty }
                ));
            }

            if (DateTime.Compare((DateTime)otherValue, (DateTime)value) > 0)
            {
                return new ValidationResult(string.Format(
                    CultureInfo.CurrentCulture,
                    "Property is not after {0}",
                    new[] { _otherProperty }
                ));
            }

            return ValidationResult.Success;
        }
    }

    public class Session
    {
        public int Id { get; set; }
        
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Start { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime End { get; set; }

        [Display(Name = "Trainee")]
        public string TraineeId { get; set; }
        [Display(Name = "Trainee")]
        public virtual BefitUser? Trainee { get; set; }
    }
}
