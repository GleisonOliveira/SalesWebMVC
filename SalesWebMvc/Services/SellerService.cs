using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> findAll()
        {
            return _context.Seller.ToList();
        }

        public void insert(Seller seller)
        {
            _context.Seller.Add(seller);
            _context.SaveChanges();
        }

        public Seller findById(int id)
        {
            var seller = _context.Seller.Include(obj => obj.Department).FirstOrDefault(item => item.Id == id);

            if(seller == null)
            {
                throw new NotFoundException("The seller was not founded");
            }

            return seller;
        }

        public void remove(int id)
        {
            _context.Seller.Remove(findById(id));
            _context.SaveChanges();
        }

        public void update(Seller seller)
        {
            bool hasAny = _context.Seller.Any(x => x.Id == seller.Id);

            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(seller);
                _context.SaveChanges();
            } catch (DbUpdateConcurrencyException e)
            {
                throw new DBConcurrencyUpdateException(e.Message);
            }
        }
    }
}
