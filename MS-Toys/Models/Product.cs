using System;
using System.ComponentModel.DataAnnotations;

namespace MS_Toys.Models
{
    public class Product
    {
        [Key]
        [Required]
        public long Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public Decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
