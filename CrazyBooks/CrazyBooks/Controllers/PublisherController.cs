using CrazyBooks_DataAccess.Repository.IRepository;
using CrazyBooks_Models.Models;
using CrazyBooks_Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrazyBooks.Controllers
{
  public class PublisherController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PublisherController> _logger;

    public PublisherController(IUnitOfWork unitOfWork, ILogger<PublisherController> logger)
    {
      _unitOfWork = unitOfWork;
      _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
      IEnumerable<Publisher> PublisherList = await _unitOfWork.Publisher.GetAllAsync();

      return View(PublisherList);
    }

        //GET - UPSERT
        public async Task<IActionResult> Upsert(int? id)
        {
            Publisher publisher = new Publisher();
            if (id == null)
            {
                //CREATE
                return View(publisher);
            }
            else
            {
                //EDIT
                publisher = await _unitOfWork.Publisher.GetAsync(id.GetValueOrDefault());
                if (publisher == null)
                {
                    return NotFound();
                }
                return View(publisher);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                if (publisher.Id == 0)
                {
                    await _unitOfWork.Publisher.AddAsync(publisher);

                }
                else
                {
                    _unitOfWork.Publisher.Update(publisher);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }


        public async Task<IActionResult> Detail(int id)
    {
      PublisherVM PublisherVM = new PublisherVM()
      {
        Publisher = await _unitOfWork.Publisher.FirstOrDefaultAsync(p => p.Id == id),
        BooksList = await _unitOfWork.Book.GetAllAsync(b => b.Publisher_Id == id, includeProperties: "Subject")

      };
      return View(PublisherVM);
    }

        //GET DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = await _unitOfWork.Publisher.GetAsync(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST DELETE
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeletePost(int? id)
        {
            var obj = await _unitOfWork.Publisher.GetAsync(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }

            await _unitOfWork.Publisher.RemoveAsync(obj);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }
    }
}
