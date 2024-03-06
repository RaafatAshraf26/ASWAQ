using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Order
    {

        [ForeignKey("Customer")]
        public string? CustomerID { get; set; }
        public Customer? Customer { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public Product? Product { get; set; }

        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        public int Total { get; set; }        
    }
}
