using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProjectDemo.Entites;

namespace ProjectDemo.ViewModel
{
    public class ProductVM
    {
        public int ProductId { get; set; }

        public CategoryVM  category { get; set; }

        public string NameProduct { get; set; }

        public string ColorProduct { get; set; }

        public decimal Price { get; set; }
    }
}
