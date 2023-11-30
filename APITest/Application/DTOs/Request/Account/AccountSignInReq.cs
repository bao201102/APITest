using System.ComponentModel.DataAnnotations;

namespace APITest.Application.DTOs.Request
{
    public class AccountSignInReq
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
