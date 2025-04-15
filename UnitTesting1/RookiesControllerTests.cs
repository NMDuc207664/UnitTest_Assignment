
using Microsoft.AspNetCore.Mvc;
using Moq;
using UnitTest_Assignment1.Controllers;
using UnitTest_Assignment1.Interfaces;
using UnitTest_Assignment1.Models;

namespace UnitTesting1
{
    public class RookiesControllerTests
    {
        private Mock<IPersonService> _mockService;
        private RookiesController _controller;
        private PersonModel _testPerson;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IPersonService>();
            _controller = new RookiesController(_mockService.Object);
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
        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();  // 👈 Dispose controller after each test
        }

        [Test]
        public void GetAllPersons_ShouldReturnOkResult()
        {
            var list = new List<PersonModel> { _testPerson };
            _mockService.Setup(s => s.GetAllPersons()).Returns(list);

            var result = _controller.GetAllPersons() as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(list, result.Value);
        }

        [Test]
        public void Index_ShouldReturnViewWithModel()
        {
            var list = new List<PersonModel> { _testPerson };
            _mockService.Setup(s => s.GetAllPersons()).Returns(list);

            var result = _controller.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(list, result.Model);
        }

        [Test]
        public void Details_ShouldReturnView_WhenFound()
        {
            _mockService.Setup(s => s.GetPersonById(_testPerson.Id)).Returns(_testPerson);

            var result = _controller.Details(_testPerson.Id) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(_testPerson, result.Model);
        }

        [Test]
        public void Details_ShouldReturnNotFound_WhenNotFound()
        {
            _mockService.Setup(s => s.GetPersonById(It.IsAny<Guid>())).Returns((PersonModel)null);

            var result = _controller.Details(Guid.NewGuid());

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Create_Post_ShouldRedirect_WhenModelIsValid()
        {
            var result = _controller.Create(_testPerson) as RedirectToActionResult;

            _mockService.Verify(s => s.AddPerson(_testPerson), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }

        [Test]
        public void Create_Post_ShouldReturnView_WhenModelIsInvalid()
        {
            _controller.ModelState.AddModelError("FirstName", "Required");

            var result = _controller.Create(_testPerson) as ViewResult;

            Assert.AreEqual(_testPerson, result.Model);
        }

        [Test]
        public void Edit_Get_ShouldReturnView_WhenFound()
        {
            _mockService.Setup(s => s.GetPersonById(_testPerson.Id)).Returns(_testPerson);

            var result = _controller.Edit(_testPerson.Id) as ViewResult;

            Assert.AreEqual(_testPerson, result.Model);
        }

        [Test]
        public void Edit_Get_ShouldReturnNotFound_WhenNotFound()
        {
            _mockService.Setup(s => s.GetPersonById(It.IsAny<Guid>())).Returns((PersonModel)null);

            var result = _controller.Edit(Guid.NewGuid());

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Edit_Post_ShouldRedirect_WhenValid()
        {
            var result = _controller.Edit(_testPerson.Id, _testPerson) as RedirectToActionResult;

            _mockService.Verify(s => s.UpdatePerson(_testPerson), Times.Once);
            Assert.AreEqual("Index", result.ActionName);
        }

        [Test]
        public void Edit_Post_ShouldReturnNotFound_WhenIdMismatch()
        {
            var result = _controller.Edit(Guid.NewGuid(), _testPerson);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void DeleteConfirmed_ShouldRedirectToConfirmation_WhenPersonExists()
        {
            _mockService.Setup(s => s.GetPersonById(_testPerson.Id)).Returns(_testPerson);

            var result = _controller.DeleteConfirmed(_testPerson.Id) as RedirectToActionResult;

            _mockService.Verify(s => s.DeletePerson(_testPerson.Id), Times.Once);
            Assert.AreEqual("DeleteConfirmation", result.ActionName);
            Assert.AreEqual(_testPerson.FirstName + " " + _testPerson.LastName, result.RouteValues["name"]);
        }

        [Test]
        public void DeleteConfirmed_ShouldReturnNotFound_WhenNotFound()
        {
            _mockService.Setup(s => s.GetPersonById(It.IsAny<Guid>())).Returns((PersonModel)null);

            var result = _controller.DeleteConfirmed(Guid.NewGuid());

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void DeleteConfirmation_Get_ShouldReturnViewWithName()
        {
            var result = _controller.DeleteConfirmation("John Doe") as ViewResult;

            Assert.AreEqual("John Doe", result.ViewData["DeletedPersonName"]);
        }
        [Test]
        public void Create_Get_ShouldReturnView()
        {
            var result = _controller.Create();
            Assert.IsInstanceOf<ViewResult>(result);
        }
        [Test]
        public void Index_ShouldReturnErrorView_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("FakeError", "Some error");

            var result = _controller.Index() as ViewResult;

            Assert.AreEqual("Error", result.ViewName);
        }
        [Test]
        public void Edit_Post_ShouldReturnView_WhenInvalid()
        {
            _controller.ModelState.AddModelError("Fake", "Invalid");
            var result = _controller.Edit(_testPerson.Id, _testPerson) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(_testPerson, result.Model);
        }
    }
}