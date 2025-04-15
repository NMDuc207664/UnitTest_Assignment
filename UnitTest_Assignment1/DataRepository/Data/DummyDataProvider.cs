using UnitTest_Assignment1.Models;
using System;
using System.Collections.Generic;

namespace UnitTest_Assignment1.Data
{
    public static class DummyDataProvider
    {
        public static List<PersonModel> GetDummyData()
        {
            return new List<PersonModel>
            {
                new PersonModel { Id = Guid.NewGuid(), FirstName = "Duc", LastName = "Nguyen", Gender = Gender.Male, DOB = new DateTime(2032, 10, 2), PhoneNumber = "0961040375", BirthPlace = "Ha Noi", IsGraduated = true },
                new PersonModel { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Đoan", Gender = Gender.Female, DOB = new DateTime(2000, 10, 15), PhoneNumber = "0961040375", BirthPlace = "Ho Chi Minh", IsGraduated = false },
                new PersonModel { Id = Guid.NewGuid(), FirstName = "Alice", LastName = "Johnson", Gender = Gender.Female, DOB = new DateTime(1998, 8, 25), PhoneNumber = "0961040375", BirthPlace = "Da Nang", IsGraduated = true },
                new PersonModel { Id = Guid.NewGuid(), FirstName = "Bob", LastName = "Brown", Gender = Gender.Male, DOB = new DateTime(2001, 3, 12), PhoneNumber = "111222333", BirthPlace = "Can Tho", IsGraduated = false }
            };
        }
    }
}
