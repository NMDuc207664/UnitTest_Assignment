using System;
using System.ComponentModel.DataAnnotations;

public class DOBValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime dob)
        {
            if (dob > DateTime.Now)
            {
                return new ValidationResult(ErrorMessage ?? "Date of Birth cannot be in the future.");
            }
        }

        return ValidationResult.Success!;
    }
}
