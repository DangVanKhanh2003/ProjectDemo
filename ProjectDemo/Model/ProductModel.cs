using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectDemo.Model
{
    public class ProductModel
    {
        public int CategoryId { get; set; }

        public string NameProduct { get; set; }

        public string ColorProduct { get; set; }

        public decimal Price { get; set; }
    }
}
