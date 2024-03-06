using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Employee
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("Id")]
        public virtual ApplicationUser? User { get; set; }

        public decimal Salary { get; set; }

        public string? Address { get; set; }        

        public int Age { get; set; }        

        public string? SuperVisor_Id { get; set; }
        [ForeignKey("SuperVisor_Id")]
        public virtual Employee? SuperVisor { get; set; }

        public virtual ICollection<WorkIn>? EmployeeSections { get; set; }
    }
}
