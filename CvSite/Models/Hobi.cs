namespace CvSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Hobi")]
    public partial class Hobi
    {
        [Key]
        public int hob_id { get; set; }

        [StringLength(50)]
        public string hob_adi { get; set; }

        public int? user_id { get; set; }

        public virtual User User { get; set; }
    }
}
