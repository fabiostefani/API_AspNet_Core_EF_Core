using fabiostefani.io.MapaCatalog.Api.Data;
using fabiostefani.io.MapaCatalog.Api.Models;
using fabiostefani.io.MapaCatalog.Api.ViewModels.ProductViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fabiostefani.io.MapaCatalog.Api.Repositories
{
    public class ProductRepository
    {
        private readonly StoreDataContext _context;

        public ProductRepository(StoreDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ListProductViewModel>> Get()
        {
            return await _context.Products
                .Include(x => x.Category)
                .Select(x => new ListProductViewModel()
                {
                    Id = x.Id,
                    Category = x.Category.Title,
                    CategoryId = x.CategoryId,
                    Price = x.Price,
                    Title = x.Title
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product> Get(int id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product> Find(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public void Save(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context.Entry<Product>(product).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
