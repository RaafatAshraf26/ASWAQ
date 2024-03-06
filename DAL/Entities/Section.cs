using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{ 
    public class Section
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Name { get; set; }
        
        public byte[]? Image { get; set; }

        public virtual ICollection<Product>? Products { get; set; }

        public virtual ICollection<WorkIn>? EmployeeSections { get; set; }
    }
}
