
// Source - https://stackoverflow.com/a
// Posted by Darin Dimitrov, modified by community. See post 'Timeline' for change history
// Retrieved 2025-11-28, License - CC BY-SA 3.0

public class RequiredIfOtherFieldIsNullAttribute : ValidationAttribute, IClientValidatable
{
    private readonly string _otherProperty;
    public RequiredIfOtherFieldIsNullAttribute(string otherProperty)
    {
        _otherProperty = otherProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var property = validationContext.ObjectType.GetProperty(_otherProperty);
        if (property == null)
        {
            return new ValidationResult(string.Format(
                CultureInfo.CurrentCulture,
                "Unknown property {0}",
                new[] { _otherProperty }
            ));
        }
        var otherPropertyValue = property.GetValue(validationContext.ObjectInstance, null);

        if (otherPropertyValue == null || otherPropertyValue as string == string.Empty)
        {
            if (value == null || value as string == string.Empty)
            {
                return new ValidationResult(string.Format(
                    CultureInfo.CurrentCulture,
                    FormatErrorMessage(validationContext.DisplayName),
                    new[] { _otherProperty }
                ));
            }
        }
        return ValidationResult.Success;
    }

    public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
    {
        var rule = new ModelClientValidationRule
        {
            ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
            ValidationType = "requiredif",
        };
        rule.ValidationParameters.Add("other", _otherProperty);
        yield return rule;
    }
}


public class MyViewModel
{
    [RequiredIfOtherFieldIsNull("Mobile")]
    public string Phone { get; set; }

    public string Mobile { get; set; }
}
