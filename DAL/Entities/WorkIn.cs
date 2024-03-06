using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class WorkIn
    {
        public string EID { get; set; }
        [ForeignKey("EID")]
        public virtual Employee? Employee { get; set; }


        public int SID { get; set; }
        [ForeignKey("SID")]
        public Section? Section { get; set; }
    }
}
