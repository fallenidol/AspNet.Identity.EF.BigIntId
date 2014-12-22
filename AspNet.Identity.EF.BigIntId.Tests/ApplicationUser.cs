using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNet.Identity.EF.BigIntId.Tests
{
    public class ApplicationUser : ApplicationUserBase
    {
        [Column("FIRSTNAME")]
        [StringLength(128)]
        public string Firstname { get; set; }

        [Column("SURNAME")]
        [StringLength(128)]
        public string Surname { get; set; }
    }
}