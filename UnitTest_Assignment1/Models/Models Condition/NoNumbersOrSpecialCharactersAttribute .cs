using System.ComponentModel.DataAnnotations;

public class NoNumbersOrSpecialCharactersAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value == null)
            return false;  // If value is null, consider it invalid
        string input = value.ToString();

        // If input is null or empty, return false
        if (string.IsNullOrEmpty(input))
            return false;

        // Check if string contains digits or special characters
        return !input.Any(c => Char.IsDigit(c) || !Char.IsLetter(c) && !Char.IsWhiteSpace(c));
    }
}
