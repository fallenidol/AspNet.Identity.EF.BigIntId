using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNet.Identity.IntegerKeys.Test
{
    public class CustomUser : IdentityUserWithIntKey
    {
        [Column("FIRSTNAME")]
        [StringLength(128)]
        public string Firstname { get; set; }

        [Column("SURNAME")]
        [StringLength(128)]
        public string Surname { get; set; }

        [Column("LAST_UPDATED_UTC")]
        [DataType(DataType.DateTime)]
        public DateTime LastUpdatedUtc { get; set; }

        [Column("SOME_DATE_UTC")]
        [DataType(DataType.DateTime)]
        public DateTime? SomeDate { get; set; }
    }
}