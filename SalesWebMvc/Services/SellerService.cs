using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Exceptions;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> findAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task insertAsync(Seller seller)
        {
            _context.Seller.Add(seller);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> findByIdAsync(int id)
        {
            var seller = await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(item => item.Id == id);

            if(seller == null)
            {
                throw new NotFoundException("The seller was not founded");
            }

            return seller;
        }

        public async Task<Seller> findByEmailAsync(string email)
        {
            var seller = await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(item => item.Email == email);

            if (seller == null)
            {
                throw new NotFoundException("The email is already registered");
            }

            return seller;
        }

        public async Task removeAsync(int id)
        {
            _context.Seller.Remove(await findByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task updateAsync(Seller seller)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == seller.Id);

            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException e)
            {
                throw new DBConcurrencyUpdateException(e.Message);
            }
        }
    }
}
