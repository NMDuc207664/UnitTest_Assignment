using UnitTest_Assignment1.Models;

namespace UnitTest_Assignment1.Interfaces
{
    public interface IPersonRepository
    {
        List<PersonModel> GetAll();
        PersonModel GetById(Guid id);
        void Add(PersonModel person);
        void Update(PersonModel person);
        void Delete(Guid id);
    }
}
