using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Models
{
    public class ApplicationUser
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required, MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }
       
    }
}
