using Microsoft.AspNetCore.Mvc;
using ClickParty.Data;
using ClickParty.Models;

namespace ClickParty.Controllers;

public class EventsController : Controller
{
  
  private readonly AppDbContext _context;
  
  public EventsController(AppDbContext context)
  {
    _context = context;
  }
  
  public IActionResult Index()
  {
    var ev = _context.events //vista user
      .Where(e => e.Status != "Deleted")
      .ToList();
    return View(ev);
  }
  
  public IActionResult Admin()
  {
    
    var ev = _context.events.ToList();  //vista admin
    return View(ev);
  }

  public IActionResult Show(int id) //detalles
  {
    var ev = _context.events.Find(id);
    return View(ev);
  }

  public IActionResult Create()
  {
    return View();
  }

  //post
  [HttpPost]
  public IActionResult Store(Event ev, string date, string action)
  {
    // Validaciones 
    if (string.IsNullOrWhiteSpace(ev.Title))
      ModelState.AddModelError("Title", "Title is required");

    if (string.IsNullOrWhiteSpace(ev.Description))
      ModelState.AddModelError("Description", "Description is required");

    if (string.IsNullOrWhiteSpace(ev.Location))
      ModelState.AddModelError("Location", "Location is required");

    if (string.IsNullOrWhiteSpace(ev.Category))
      ModelState.AddModelError("Category", "Category is required");

    if (string.IsNullOrWhiteSpace(date))
      ModelState.AddModelError("Date", "Date is not valid");

    // Titulo duplicado
    bool titleExists = _context.events.Any(e => e.Title == ev.Title);
    if (titleExists)
      ModelState.AddModelError("Title", "This title already exists");

    if (!ModelState.IsValid)
      return View("Create", ev);

    _context.events.Add(ev);
    _context.SaveChanges();

    return RedirectToAction("Admin");
  }

  //get
  public IActionResult Edit(int id) //update
  {
    var ev = _context.events.Find(id);
    if (ev == null) return NotFound();
    return View(ev);
  }

  //post
  [HttpPost]
  public IActionResult Update(Event ev, string date, string action)
  {
    if (string.IsNullOrWhiteSpace(ev.Title))
      ModelState.AddModelError("Title", "Title is required");

    if (string.IsNullOrWhiteSpace(ev.Description))
      ModelState.AddModelError("Description", "Description is required");

    if (string.IsNullOrWhiteSpace(ev.Location))
      ModelState.AddModelError("Location", "Location is required");

    if (string.IsNullOrWhiteSpace(ev.Category))
      ModelState.AddModelError("Category", "Category is required");

    if (string.IsNullOrWhiteSpace(date))
      ModelState.AddModelError("Date", "Date is not valid");

    // Título duplicado sin el que esta editando
    bool titleExists = _context.events.Any(e => e.Title == ev.Title && e.Id != ev.Id);
    if (titleExists)
      ModelState.AddModelError("Title", "This title already exists");

    if (!ModelState.IsValid)
      return View("Edit", ev);

    _context.events.Update(ev);
    _context.SaveChanges();

    return RedirectToAction("Admin");
  }
  
  //post -> actualiza  pero no elimina
  [HttpPost]
  public IActionResult Destroy(int id)
  {
    var ev = _context.events.Find(id);

    if (ev == null)
      return NotFound();

    ev.Status = "Deleted";

    _context.events.Update(ev);
    _context.SaveChanges();

    return RedirectToAction("Admin");
  }

}





