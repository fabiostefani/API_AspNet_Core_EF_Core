using fabiostefani.io.MapaCatalog.Api.Data;
using fabiostefani.io.MapaCatalog.Api.Models;
using fabiostefani.io.MapaCatalog.Api.ViewModels;
using fabiostefani.io.MapaCatalog.Api.ViewModels.ProductViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fabiostefani.io.MapaCatalog.Api.Controllers
{
    public class ProductController
    {
        private readonly StoreDataContext _context;
        public ProductController(StoreDataContext context)
        {
            _context = context;
        }

        [Route("v1/products")]
        [HttpGet]
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

        [Route("v1/products/{id}")]
        [HttpGet]
        public Product Get(int id)
        {
            return _context.Products.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        [Route("v1/products")]
        [HttpPost]
        public ResultViewModel Post([FromBody]EditorProductViewModel productViewModel)
        {
            var product = new Product()
            {
                CategoryId = productViewModel.CategoryId,
                //CreateDate = DateTime.Now,
                Description = productViewModel.Description,
                Image = productViewModel.Image,
                //LastUpdateDate = DateTime.Now,
                Price = productViewModel.Price,
                Quantity = productViewModel.Quantity,
                Title = productViewModel.Title,
            };
            _context.Products.Add(product);
            _context.SaveChanges();
            return new ResultViewModel()
            {
                Success = true,
                Data = product,
                Message = "Operação realizada com sucesso."
            };
        }

        [Route("v1/products")]
        [HttpPut]
        public ResultViewModel Put([FromBody]EditorProductViewModel productViewModel)
        {

            var product = _context.Products.Find(productViewModel.Id);
            product.CategoryId = productViewModel.CategoryId;            
            product.Description = productViewModel.Description;
            product.Image = productViewModel.Image;
            product.LastUpdateDate = DateTime.Now;
            product.Price = productViewModel.Price;
            product.Quantity = productViewModel.Quantity;
            product.Title = productViewModel.Title;

            _context.Entry<Product>(product).State = EntityState.Modified;
            _context.SaveChanges();

            return new ResultViewModel()
            {
                Success = true,
                Data = product,
                Message = "Operação realizada com sucesso."
            };
        }

        [Route("v1/products")]
        [HttpDelete]
        public Product Delete([FromBody]Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();

            return product;
        }
    }
}
