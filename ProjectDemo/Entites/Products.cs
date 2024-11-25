using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDemo.Entites
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string NameProduct { get; set; }

        [Required]
        public string ColorProduct { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }


        [ForeignKey("CategoryId")]
        public Categories Categories { get; set; }


    }
}
