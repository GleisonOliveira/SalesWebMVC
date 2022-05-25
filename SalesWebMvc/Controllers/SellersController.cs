using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Exceptions;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using System.Diagnostics;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.findAllAsync();

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return await getViewWithDeparmentsAsync();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if(!ModelState.IsValid)
            {
                return await getViewWithDeparmentsAsync(seller);
            }

            await _sellerService.insertAsync(seller);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.removeAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (AbstractException e)
            {
                return RedirectToError(e);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                return View(await _sellerService.findByIdAsync(id));
            }
            catch (AbstractException e)
            {
                return RedirectToError(e);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var seller = await _sellerService.findByIdAsync(id);

                return await getViewWithDeparmentsAsync(seller);
            }
            catch (AbstractException e)
            {
                return RedirectToError(e);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                return await getViewWithDeparmentsAsync(seller);
            }

            try
            {
                await _sellerService.updateAsync(seller);

                return RedirectToAction(nameof(Index));
            }
            catch (AbstractException e)
            {
                return RedirectToError(e);
            }
        }

        public IActionResult Error(string message, int httpStatus)
        {
            return View(
                new ErrorViewModel
                {
                    Message = message,
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    HttpStatus = httpStatus
                }
           );
        }

        private IActionResult RedirectToError(AbstractException e)
        {
            return RedirectToAction(nameof(Error), new { message = e.Message, httpStatus = e.HttpStatus });
        }

        private async Task<ViewResult> getViewWithDeparmentsAsync(Seller seller = null)
        {
            var deparments = await _departmentService.findAllAsync();

            return View(new SellerFormViewModel { Departments = deparments, Seller = seller });
        }
    }
}
