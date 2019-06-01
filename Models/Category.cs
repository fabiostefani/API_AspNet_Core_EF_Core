using System.Collections.Generic;

namespace fabiostefani.io.MapaCatalog.Api.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
