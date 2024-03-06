using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public decimal Discout { get; set; }

        public byte[]? Image { get; set; }

        public List<Order>? Orders { get; set; }

        public int SID { get; set; }
        [ForeignKey("SID")]
        public virtual Section? Section { get; set; }
    }
}
