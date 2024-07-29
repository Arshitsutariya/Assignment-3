using System.ComponentModel.DataAnnotations;

namespace Assign3.Models
{
    public class Comment 
    {
        [Key] 
        public int Id {get; set;}
        [Required]
        public String product_id {get; set;}
        [Required]
        public String user_id {get; set;}
        public String? rating  {get; set;}
        public String? images {get; set;}
        public String? comment {get; set;}
    }
}