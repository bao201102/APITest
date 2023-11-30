using System.ComponentModel.DataAnnotations;

namespace APITest.Application.DTOs.Request
{
    public class AccountSignUpReq
    {
        [Required]
        [EmailAddress]
        public string email { get; set; } = string.Empty;
        [Required]
        public string password { get; set; } = string.Empty;
        [Required]
        public string name { get; set; } = string.Empty;
    }
}
