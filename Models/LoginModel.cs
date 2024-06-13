using System.ComponentModel.DataAnnotations;

namespace EffectiveWebProg.Models
{
    public class LoginModel
    {
        // [Required]
        // [StringLength(50, MinimumLength = 3)]
        // public required string UserUsername { get; set; }
        public required string UserEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string UserPassword { get; set; }

        public bool RememberMe { get; set; }
    }
}
