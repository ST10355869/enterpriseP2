using System.ComponentModel.DataAnnotations;

namespace enterpriseP2.Models
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }
    public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public DateOnly DateAdded { get; set; }
    }
}
