using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreApiWithSwagger.Models
{
    public class LoginRequest : IValidatableObject
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Email == Password)
            {
                yield return new ValidationResult("Password cannot be the same as Email", new List<string>() { "Password" });
            }
        }
    }
}
