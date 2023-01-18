using System.ComponentModel.DataAnnotations;

namespace MS_Toys.Models
{
    public class Transaction
    {
        [Key]
        [Required]
        public long Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
