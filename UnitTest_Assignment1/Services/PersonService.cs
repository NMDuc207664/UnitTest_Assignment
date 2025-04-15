using Microsoft.Extensions.Caching.Memory;  // Đảm bảo thêm thư viện này
using UnitTest_Assignment1.Models;
using UnitTest_Assignment1.Interfaces;
namespace UnitTest_Assignment1.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;//tại sao khi thêm Imemory thì personservice ko thêm para sẽ báo null?
        private const string CacheKey = "PersonsList";  // Cache key để lưu danh sách người
        private List<PersonModel> _persons;

        public PersonService(IPersonRepository personRepository)//sử dụng IMemoryCache đăng ký
        {
            _personRepository = personRepository;
        }
        public List<PersonModel> GetAllPersons()
        {
            // Lấy dữ liệu từ bộ nhớ cache
            return _personRepository.GetAll();
        }

        public PersonModel GetPersonById(Guid id)
        {
            //var persons = GetAllPersons();
            return _personRepository.GetById(id);
        }

        public void AddPerson(PersonModel person)
        {
            _personRepository.Add(person);
        }

        public void UpdatePerson(PersonModel person)
        {
            // var persons = GetAllPersons();
            _personRepository.Update(person);
        }

        public void DeletePerson(Guid id)
        {
            _personRepository.Delete(id);
        }
    }
}