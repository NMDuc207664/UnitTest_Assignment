using Moq;
using UnitTest_Assignment1.Interfaces;
using UnitTest_Assignment1.Models;
using UnitTest_Assignment1.Services;

namespace UnitTesting1
{
    public class PersonServiceTests
    {
        private Mock<IPersonRepository> _mockRepo;
        private PersonService _personService;
        private PersonModel _testPerson;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IPersonRepository>();
            _personService = new PersonService(_mockRepo.Object);
            _testPerson = new PersonModel
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "0948611868",
                BirthPlace = "USA",
                Gender = Gender.Male,
                DOB = new DateTime(1990, 1, 1),
                IsGraduated = true
            };
        }

        [Test]
        public void GetAllPersons_ShouldReturnAllPersons()
        {
            var persons = new List<PersonModel> { _testPerson };
            _mockRepo.Setup(r => r.GetAll()).Returns(persons);

            var result = _personService.GetAllPersons();

            Assert.AreEqual(persons, result);
        }

        [Test]
        public void GetPersonById_ShouldReturnCorrectPerson()
        {
            _mockRepo.Setup(r => r.GetById(_testPerson.Id)).Returns(_testPerson);

            var result = _personService.GetPersonById(_testPerson.Id);

            Assert.AreEqual(_testPerson, result);
        }

        [Test]
        public void AddPerson_ShouldCallRepository()
        {
            _personService.AddPerson(_testPerson);
            _mockRepo.Verify(r => r.Add(_testPerson), Times.Once);
        }

        [Test]
        public void UpdatePerson_ShouldCallRepository()
        {
            _personService.UpdatePerson(_testPerson);
            _mockRepo.Verify(r => r.Update(_testPerson), Times.Once);
        }

        [Test]
        public void DeletePerson_ShouldCallRepository()
        {
            _personService.DeletePerson(_testPerson.Id);
            _mockRepo.Verify(r => r.Delete(_testPerson.Id), Times.Once);
        }
        [Test]
        public void GetPersonById_ShouldReturnNull_WhenNotFound()
        {
            _mockRepo.Setup(r => r.GetById(It.IsAny<Guid>())).Returns((PersonModel)null);

            var result = _personService.GetPersonById(Guid.NewGuid());

            Assert.IsNull(result);
        }

    }
}