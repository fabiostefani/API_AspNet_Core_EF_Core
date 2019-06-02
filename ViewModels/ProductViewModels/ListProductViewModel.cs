using fabiostefani.io.MapaCatalog.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fabiostefani.io.MapaCatalog.Api.ViewModels.ProductViewModels
{
    public class ListProductViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }        
        public int CategoryId { get; set; }
        public string Category { get; set; }
    }
}
