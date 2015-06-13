using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNet.Identity.IntegerKeys.Test
{
    public class CustomUser : IdentityUser
    {
        [Column("FIRSTNAME")]
        [StringLength(128)]
        public string Firstname { get; set; }

        [Column("SURNAME")]
        [StringLength(128)]
        public string Surname { get; set; }
    }
}