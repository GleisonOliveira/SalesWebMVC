using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> findByDateAsync(DateTime? initialDate, DateTime? finalDate)
        {
            var result = from obj in _context.SalesRecord select obj;

            if(initialDate.HasValue)
            {
                result = result.Where(x => x.Date >= initialDate.Value);
            }

            if (finalDate.HasValue)
            {
                result = result.Where(x => x.Date <= finalDate.Value);
            }

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Department,SalesRecord>>> findByDateGroupedAsync(DateTime? initialDate, DateTime? finalDate)
        {
            var result = from obj in _context.SalesRecord select obj;

            if (initialDate.HasValue)
            {
                result = result.Where(x => x.Date >= initialDate.Value);
            }

            if (finalDate.HasValue)
            {
                result = result.Where(x => x.Date <= finalDate.Value);
            }

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Department)
                .ToListAsync();
        }
    }
}
