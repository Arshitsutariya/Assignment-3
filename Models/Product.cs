using System.ComponentModel.DataAnnotations;

namespace Assign3.Models
{
    public class Product 
    {
        [Key] 
        public int Id {get; set;}
        [Required]
        public String description {get; set;}
        public String? image {get; set;}
        public String? price  {get; set;}
        public String? quantity {get; set;}
    }
}