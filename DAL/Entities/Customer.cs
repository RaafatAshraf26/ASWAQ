using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;

namespace DAL.Entities
{
    public class Customer
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("Id")]
        public virtual ApplicationUser? User { get; set; }

        
        public List<Order>? Orders { get; set; }
    }
}
