using System.ComponentModel.DataAnnotations;

namespace Assign3.Models
{
    public class Order 
    {
        [Key] 
        public int Id {get; set;}
        [Required]
        public String product_id {get; set;}
        public String? quantity {get; set;}
        [Required]
        public String user_id  {get; set;}
        [Required]
        public String order_date {get; set;}
   }
}