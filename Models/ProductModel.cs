using System.ComponentModel.DataAnnotations;

namespace enterpriseP2.Models
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public double Price { get; set; }
        
        //grab current date when product is added
        public DateOnly DateAdded { get; set; }

        public int FarmerId { get; set; }

    }
}
