using CrazyBooks_Models.Models;
using CrazyBooks_DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrazyBooks_DataAccess.Data;
using CrazyBooks_DataAccess.Repository.IRepository;

namespace CrazyBooks.Controllers
{
  // Utilisation des méthodes asynchrones
  public class SubjectController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SubjectController> _logger;

    public SubjectController(IUnitOfWork unitOfWork, ILogger<SubjectController> logger)
    {
      _unitOfWork = unitOfWork;
      _logger = logger;
    }
    public async Task<IActionResult> Index()
    {
      IEnumerable<Subject> SubjectList = await _unitOfWork.Subject.GetAllAsync();

      return View(SubjectList);
    }

    //GET CREATE
    public IActionResult Create()
    {
     return View();
    }

    //POST CREATE
    [HttpPost]
    public async Task<IActionResult> Create(Subject subject)
    {
      if (ModelState.IsValid)
      {
        // Ajouter à la BD
       await _unitOfWork.Subject.AddAsync(subject);
     
        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
      }
      return this.View(subject);
    }

        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            Subject subject = new Subject();
                subject = await _unitOfWork.Subject.GetAsync(id.GetValueOrDefault());
                if (subject == null)
                {
                    return NotFound();
                }
                return View(subject);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Subject subject)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Subject.Update(subject);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        //GET DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = await _unitOfWork.Subject.GetAsync(id.GetValueOrDefault());
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
            var obj = await _unitOfWork.Subject.GetAsync(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }

            await _unitOfWork.Subject.RemoveAsync(obj);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

    }
}
