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
    public class AuthorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(IUnitOfWork unitOfWork, ILogger<AuthorController> logger)
    {
            _unitOfWork = unitOfWork;
            _logger = logger;
    }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Author> AuthorList = await _unitOfWork.Author.GetAllAsync();

            return View(AuthorList);
        }

        //GET - UPSERT
        public async Task<IActionResult> Upsert(int? id)
        {
            AuthorVM authorVM = new AuthorVM()
            {
             Author = new Author()
            };

            if (id == null)
            {
                //CREATE
                return View(authorVM);
            }
            else
            {
                //EDIT
                authorVM.Author = await _unitOfWork.Author.FirstOrDefaultAsync(filter: a => a.Id == id, includeProperties:"AuthorDetail");
                
                if (authorVM == null)
                {
                    return NotFound();
                }
                return View(authorVM);
            }

        }

        //POST Upsert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(AuthorVM authorVM)
        {
            if (authorVM.Author.Id == 0)
            {
                //this is create
                await _unitOfWork.Author.AddAsync(authorVM.Author);
            }
            else
            {
                //this is an update
                _unitOfWork.Author.Update(authorVM.Author);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }


        //GET DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = await _unitOfWork.Author.GetAsync(id.GetValueOrDefault());
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
            var obj = await _unitOfWork.Author.GetAsync(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }

            await _unitOfWork.Author.RemoveAsync(obj);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }
    }
}
