using UnitTest_Assignment1.Models;
namespace UnitTest_Assignment1.Interfaces
{
    public interface IPersonService
    {
        List<PersonModel> GetAllPersons();
        PersonModel? GetPersonById(Guid id);
        void AddPerson(PersonModel person);
        void UpdatePerson(PersonModel person);
        void DeletePerson(Guid id);
    }
}