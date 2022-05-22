using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Exceptions;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var list = _sellerService.findAll();

            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var deparments = _departmentService.findAll();

            return View(new SellerFormViewModel { Departments = deparments });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.insert(seller);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                _sellerService.remove(id);

                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                return View(_sellerService.findById(id));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var seller = _sellerService.findById(id);
                var deparments = _departmentService.findAll();

                return View(new SellerFormViewModel { Departments = deparments, Seller = seller });
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            try
            {
                _sellerService.update(seller);

                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DBConcurrencyUpdateException)
            {
                return BadRequest();
            }
        }
    }
}
