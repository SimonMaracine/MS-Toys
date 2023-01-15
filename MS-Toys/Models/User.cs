using MS_Toys.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

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
