using Microsoft.Extensions.Caching.Memory;
using UnitTest_Assignment1.Interfaces;
using UnitTest_Assignment1.Models;
using UnitTest_Assignment1.Data;

namespace UnitTest_Assignment1.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        // Tạo một instance của MemoryCache duy nhất (Singleton)
        private static readonly IMemoryCache _memoryCache = new MemoryCache(new MemoryCacheOptions()); // Singleton instance
        private const string CacheKey = "PersonsList";
        private List<PersonModel> _persons;
        // Constructor
        public PersonRepository()
        {
            // Kiểm tra xem dữ liệu đã có trong cache chưa, nếu chưa thì tạo mới
            if (!_memoryCache.TryGetValue(CacheKey, out _persons))
            {
                //_persons = new List<PersonModel>(); // Khởi tạo danh sách rỗng nếu không có trong cache
                _persons = DummyDataValidator.GetValidatedDummyData();

                // Nếu dữ liệu mẫu có sẵn, cập nhật _lastId bằng giá trị max hiện có, nếu không thì để _lastId = 0
                _memoryCache.Set(CacheKey, _persons, TimeSpan.FromHours(1)); // Lưu vào cache
            }
        }

        public List<PersonModel> GetAll()
        {
            if (_persons == null)
            {
                Console.WriteLine("[DEBUG] create a new list if no data in cache.");
                return new List<PersonModel>();
            }
            return _persons;
        }

        public PersonModel GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                Console.WriteLine($"[ERROR] ID không hợp lệ: {id}");
                return null;
            }

            return _persons.First(p => p.Id == id);
        }

        public void Add(PersonModel person)
        {
            if (person == null)
            {
                Console.WriteLine("[ERROR] Dữ liệu đầu vào null, không thể thêm.");
                return;
            }
            // Gán ID mới bằng Guid. Mỗi lần gọi Guid.NewGuid() sẽ tạo ra một giá trị duy nhất.
            person.Id = Guid.NewGuid();
            _persons.Insert(0, person);
            _memoryCache.Set(CacheKey, _persons, TimeSpan.FromHours(1));
            Console.WriteLine($"[INFO] Đã thêm người: {person.FirstName} {person.LastName} (ID: {person.Id})");
        }

        public void Update(PersonModel person)
        {
            if (person == null || person.Id == Guid.Empty)
            {
                Console.WriteLine("[ERROR] Dữ liệu đầu vào không hợp lệ, không thể cập nhật.");
                return;
            }  // Kiểm tra dữ liệu hợp lệ
            PersonModel existingPerson = GetById(person.Id);
            if (existingPerson != null)
            {
                existingPerson.FirstName = person.FirstName;
                existingPerson.LastName = person.LastName;
                existingPerson.Gender = person.Gender;
                existingPerson.DOB = person.DOB;
                existingPerson.PhoneNumber = person.PhoneNumber;
                existingPerson.BirthPlace = person.BirthPlace;
                existingPerson.IsGraduated = person.IsGraduated;

                _memoryCache.Set(CacheKey, _persons, TimeSpan.FromHours(1)); // Cập nhật cache
            }
            else
            {
                Console.WriteLine($"[WARNING] Không tìm thấy người có ID: {person.Id} để cập nhật.");
            }
        }

        public void Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                Console.WriteLine($"[ERROR] ID không hợp lệ: {id}, không thể xóa.");
                return;
            }
            PersonModel person = GetById(id);
            if (person != null)
            {
                _persons.Remove(person);
                _memoryCache.Set(CacheKey, _persons, TimeSpan.FromHours(1)); // Cập nhật cache
            }
            else
            {
                Console.WriteLine($"[WARNING] Không tìm thấy người có ID: {id} để xóa.");
            }
        }
    }
}
