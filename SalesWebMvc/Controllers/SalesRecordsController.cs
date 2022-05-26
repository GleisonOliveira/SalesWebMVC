using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using System;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? initialDate, DateTime? finalDate)
        {
            if(initialDate.HasValue)
            {
                ViewData["initialDate"] = initialDate.Value.ToString("yyyy-MM-dd");
            }
            if(finalDate.HasValue)
            {
                ViewData["finalDate"] = finalDate.Value.ToString("yyyy-MM-dd");
            }
            
            var result = await _salesRecordService.findByDateAsync(initialDate, finalDate);

            return View(result);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? initialDate, DateTime? finalDate)
        {
            if (initialDate.HasValue)
            {
                ViewData["initialDate"] = initialDate.Value.ToString("yyyy-MM-dd");
                ViewData["minDate"] = initialDate;
            }
            if (finalDate.HasValue)
            {
                ViewData["finalDate"] = finalDate.Value.ToString("yyyy-MM-dd");
                ViewData["maxDate"] = finalDate;
            }

            var result = await _salesRecordService.findByDateGroupedAsync(initialDate, finalDate);

            return View(result);
        }
    }
}
