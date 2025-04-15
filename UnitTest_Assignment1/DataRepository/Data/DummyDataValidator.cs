using UnitTest_Assignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest_Assignment1.Data
{
    public static class DummyDataValidator
    {
        public static List<PersonModel> GetValidatedDummyData()
        {
            List<PersonModel> persons = DummyDataProvider.GetDummyData();
            persons = persons.Where(p => IsValidPerson(p)).ToList();
            return persons;
        }

        private static bool IsValidPerson(PersonModel person)
        {
            bool isValid = person.DOB <= DateTime.Now && // Ngày sinh không vượt quá hiện tại
                           IsValidName(person.FirstName) &&
                           IsValidName(person.LastName) &&
                           IsValidName(person.BirthPlace) &&
                           IsValidPhoneNumber(person.PhoneNumber);

            if (!isValid)
            {
                Console.WriteLine($"[DEBUG] Invalid data removed: {person.FirstName} {person.LastName}, DOB: {person.DOB}, Phone: {person.PhoneNumber}");
            }

            return isValid;
        }

        private static bool IsValidName(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            return input.All(c => Char.IsLetter(c) || Char.IsWhiteSpace(c)); // Chỉ chứa chữ và khoảng trắng
        }

        private static bool IsValidPhoneNumber(string phone)
        {
            return phone.Length == 10 && phone.All(Char.IsDigit); // Chỉ chứa số và đúng 10 chữ số
        }
    }
}
