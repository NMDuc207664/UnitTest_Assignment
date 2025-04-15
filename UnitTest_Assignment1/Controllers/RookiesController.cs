using Microsoft.AspNetCore.Mvc;
using UnitTest_Assignment1.Models;
using UnitTest_Assignment1.Interfaces;
namespace UnitTest_Assignment1.Controllers;

[Route("NashTech/[controller]")]
public class RookiesController : Controller
{
    private IPersonService _personService;
    public RookiesController(IPersonService personService)
    {
        _personService = personService;
    }
    [HttpGet("PersonsList")]
    public IActionResult GetAllPersons()
    {
        List<PersonModel> persons = _personService.GetAllPersons();
        return Ok(persons);
    }
    [HttpGet("Index")]
    public IActionResult Index()
    {
        var persons = _personService.GetAllPersons();

        // Kiểm tra nếu model không hợp lệ
        if (!ModelState.IsValid)
        {
            return View("Error"); // Trả về một trang lỗi hoặc view mặc định nếu model không hợp lệ
        }

        return View(persons);
    }
    [HttpGet("Details/{id}")]
    public IActionResult Details(Guid id)
    {
        var person = _personService.GetPersonById(id);
        if (person == null)
        {
            return NotFound();
        }
        return View(person);
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("Create")]
    public IActionResult Create(PersonModel person)
    {
        if (ModelState.IsValid)
        {
            // Thêm person vào danh sách hoặc cập nhật
            _personService.AddPerson(person);
            return RedirectToAction("Index");
        }
        // Nếu không hợp lệ, quay lại trang tạo với thông báo lỗi
        return View(person);
    }
    [HttpGet("Edit/{id}")]
    public IActionResult Edit(Guid id)
    {
        var person = _personService.GetPersonById(id);
        if (person == null)
        {
            return NotFound();
        }
        return View(person);
    }


    [HttpPost("Edit/{id}")]
    public IActionResult Edit(Guid id, PersonModel person)
    {
        if (id != person.Id)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            // Thêm person vào danh sách hoặc cập nhật
            _personService.UpdatePerson(person);
            return RedirectToAction("Index");
        }
        return View(person);
    }
    [HttpPost("DeleteConfirmation")]
    public IActionResult DeleteConfirmed(Guid id)
    {
        var person = _personService.GetPersonById(id);
        if (person == null)
        {
            return NotFound();
        }

        // Delete the person
        _personService.DeletePerson(id);

        // Redirect directly to the DeleteConfirmation page
        return RedirectToAction("DeleteConfirmation", new { name = person.FirstName + " " + person.LastName });
    }
    [HttpGet("DeleteConfirmation")]
    public IActionResult DeleteConfirmation(string name)
    {
        // Return the view with the person's name
        ViewBag.DeletedPersonName = name;
        return View();
    }
}