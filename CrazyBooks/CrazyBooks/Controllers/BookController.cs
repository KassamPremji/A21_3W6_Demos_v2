using CrazyBooks_DataAccess.Repository.IRepository;
using CrazyBooks_Models.Models;
using CrazyBooks_Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrazyBooks.Controllers
{
  public class BookController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SubjectController> _logger;

    public BookController(IUnitOfWork unitOfWork, ILogger<SubjectController> logger)
    {
      _unitOfWork = unitOfWork;
      _logger = logger;
    }
    public async Task<IActionResult> Index()
    {
      IEnumerable<Book> objList = await _unitOfWork.Book.GetAllAsync(includeProperties:"Publisher,Subject");

      return View(objList);
    }

    //GET CREATE
    public async Task<IActionResult> Create()
    {
      IEnumerable<Subject> SubList = await _unitOfWork.Subject.GetAllAsync();

      IEnumerable<Publisher> PubList = await _unitOfWork.Publisher.GetAllAsync();

      BookVM bookVM = new BookVM()
      {
        Book = new Book(),
        SubjectList =  SubList.Select(i => new SelectListItem
        {
          Text = i.Name,
          Value = i.Id.ToString()
        }),
        PublisherList = PubList.Select(i => new SelectListItem
        {
          Text = i.Name,
          Value = i.Id.ToString()
        })
      };
      return View(bookVM);
    }

    //POST CREATE
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BookVM bookVM)
    {
      if (ModelState.IsValid)
      {
        // Ajouter à la BD
       await _unitOfWork.Book.AddAsync(bookVM.Book);
      }

      _unitOfWork.Save();
      return RedirectToAction(nameof(Index));
    }
  }
}
