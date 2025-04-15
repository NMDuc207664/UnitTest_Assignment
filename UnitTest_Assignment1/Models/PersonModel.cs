using System.ComponentModel.DataAnnotations;

namespace UnitTest_Assignment1.Models;

public class PersonModel
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Please enter First Name.")]
    [NoNumbersOrSpecialCharacters(ErrorMessage = "First Name must only contain letters and spaces.")]
    public required string FirstName { get; set; }
    [Required(ErrorMessage = "Please enter Last Name.")]
    [NoNumbersOrSpecialCharacters(ErrorMessage = "Last Name must only contain letters and spaces.")]
    public required string LastName { get; set; }
    public Gender Gender { get; set; }
    [Required(ErrorMessage = "Please enter Date of Birth.")]
    [DOBValidation(ErrorMessage = "Date of Birth cannot be in the future.")]
    public DateTime DOB { get; set; }
    [Required(ErrorMessage = "Please enter Phone Number.")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone Number must be exactly 10 digits.")]
    public required string PhoneNumber { get; set; }
    [Required(ErrorMessage = "Please enter Birth Place Address.")]
    [NoNumbersOrSpecialCharacters(ErrorMessage = "Birth Place must only contain letters and spaces.")]
    public required string BirthPlace { get; set; }
    public bool IsGraduated { get; set; }

}
// Định nghĩa enum cho Gender
public enum Gender
{
    Male,
    Female
}