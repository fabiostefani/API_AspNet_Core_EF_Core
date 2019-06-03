using fabiostefani.io.MapaCatalog.Api.Data;
using fabiostefani.io.MapaCatalog.Api.Models;
using fabiostefani.io.MapaCatalog.Api.Repositories;
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
        private readonly ProductRepository _repository;
        public ProductController(ProductRepository repository)
        {
            _repository = repository;
        }

        [Route("v1/products")]
        [HttpGet]
        public async Task<IEnumerable<ListProductViewModel>> Get()
        {
            return await _repository.Get();
        }

        [Route("v1/products/{id}")]
        [HttpGet]
        public async Task<Product> Get(int id)
        {
            return await _repository.Get(id);
        }

        [Route("v1/products")]
        [HttpPost]
        public ResultViewModel Post([FromBody]EditorProductViewModel productViewModel)
        {
            productViewModel.Validate();
            if (productViewModel.Invalid)
            {
                return new ResultViewModel()
                {
                    Data = productViewModel.Notifications,
                    Message = "Não foi possível cadastrar o produto",
                    Success = false
                };
            }
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
            _repository.Save(product);            
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
            if (productViewModel.Invalid)
            {
                return new ResultViewModel()
                {
                    Data = productViewModel.Notifications,
                    Message = "Não foi possível cadastrar o produto",
                    Success = false
                };
            }

            var product = _repository.Find(productViewModel.Id).Result;
            product.CategoryId = productViewModel.CategoryId;            
            product.Description = productViewModel.Description;
            product.Image = productViewModel.Image;
            product.LastUpdateDate = DateTime.Now;
            product.Price = productViewModel.Price;
            product.Quantity = productViewModel.Quantity;
            product.Title = productViewModel.Title;

            _repository.Update(product);

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
            _repository.Delete(product);            

            return product;
        }
    }
}
