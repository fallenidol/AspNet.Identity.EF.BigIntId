using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNet.Identity.EntityFramework.LongId;

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
