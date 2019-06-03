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
    public class CategoryRepository
    {
        private readonly StoreDataContext _context;

        public CategoryRepository(StoreDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> Get()
        {
            return await _context.Categories                
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Category> Get(int id)
        {
            return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category> Find(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public IEnumerable<Product> GetProducts(int id)
        {
            return _context.Products.AsNoTracking().Where(x => x.CategoryId == id).ToList();
        }

        public void Save(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            _context.Entry<Category>(category).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}
