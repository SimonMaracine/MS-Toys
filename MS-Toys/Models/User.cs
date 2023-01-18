using System.ComponentModel.DataAnnotations;

namespace MS_Toys.Models
{
    public class User
    {
        [Key]
        [Required]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(30)]
        public string EncryptedPassword { get; set; }  // TODO Encrypted
    }
}
