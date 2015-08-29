using System.ComponentModel.DataAnnotations;

namespace AopDemo.Application.User
{
    public class ChangePasswordRequest
    {
        public int UserId { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}