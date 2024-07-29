using System.ComponentModel.DataAnnotations;

namespace Assign3.Models
{
    public class Cart 
    {
        [Key] 
        public int Id {get; set;}
        [Required]
        public String product_id {get; set;}
        public String? quantity {get; set;}
        [Required]
        public String user_id  {get; set;}
    }
}