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

    //GET UPSERT
    public async Task<IActionResult> Upsert(int? id)
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
        if (id == null)
        {
            //this is for create
            return View(bookVM);
        }
            //this is for edit
            bookVM.Book = await _unitOfWork.Book.GetAsync(id.GetValueOrDefault());
        if (bookVM.Book == null)
        {
            return NotFound();
        }
        return View(bookVM);
    }

    //POST CREATE
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(BookVM bookVM)
    {
        if(bookVM.Book.Id == 0)
        {
        //this is create
        await _unitOfWork.Book.AddAsync(bookVM.Book);
        }
        else
        {
        //this is an update
        _unitOfWork.Book.Update(bookVM.Book);
        }
        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
    }

  }
}
