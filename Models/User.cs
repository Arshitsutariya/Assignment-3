using System.ComponentModel.DataAnnotations;

namespace Assign3.Models
{
    public class User 
    {
        [Key]  
        public int Id {get; set;}
        [Required]
        public String email {get; set;}
        [Required]
        public String password {get; set;}
        [Required]
        public String username  {get; set;}
        public String? purchase_history {get; set;}
        public String? shipping_address {get; set;}
    }
}