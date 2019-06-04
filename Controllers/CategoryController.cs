using fabiostefani.io.MapaCatalog.Api.Data;
using fabiostefani.io.MapaCatalog.Api.Models;
using fabiostefani.io.MapaCatalog.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fabiostefani.io.MapaCatalog.Api.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _repository;
        public CategoryController(CategoryRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Transforma uma temperatura em Fahrenheit para o equivalente
        /// nas escalas Celsius e Kelvin.
        /// </summary>
        /// <param name="temperatura">Temperatura em Fahrenheit</param>
        /// <returns>Objeto contendo valores para uma temperatura
        /// nas escalas Fahrenheit, Celsius e Kelvin.</returns>
        [Route("v1/categories")]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration =60)]
        [HttpGet]
        public async Task<IEnumerable<Category>>  Get()
        {
            return await _repository.Get();
        }

        [Route("v2/categories")]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60)]
        [HttpGet]
        public IEnumerable<Category> GetSemAwait()
        {
            return _repository.Get().Result;
        }

        [Route("v1/categories/{id}")]
        [HttpGet]
        public async Task< Category> Get(int id)
        {
            return await _repository.Get(id);
        }

        [Route("v1/categories/{id}/products")]
        [HttpGet]
        public  IEnumerable<Product> GetProducts(int id)
        {
            return _repository.GetProducts(id);
        }

        [Route("v1/categories")]
        [HttpPost]
        public Category Post([FromBody]Category category)
        {
            _repository.Save(category);            
            return category;
        }

        [Route("v1/categories")]
        [HttpPut]
        public Category Put([FromBody]Category category)
        {
            _repository.Update(category);

            return category;
        }

        [Route("v1/categories")]
        [HttpDelete]
        public Category Delete([FromBody]Category category)
        {
            _repository.Delete(category);
            
            return category;
        }


    }
}
