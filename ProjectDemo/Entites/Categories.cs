using System.ComponentModel.DataAnnotations;

namespace ProjectDemo.Entites
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public ICollection<Products> Products { get; set; }

    }
}
