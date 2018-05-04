namespace CvSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Resume")]
    public partial class Resume
    {
        [Key]
        public int res_id { get; set; }

        [StringLength(250)]
        public string res_hakkimda { get; set; }

        [StringLength(250)]
        public string res_resim { get; set; }

        [StringLength(50)]
        public string res_dogum_tarihi { get; set; }

        [StringLength(250)]
        public string res_adres { get; set; }

        public int? res_user_id { get; set; }

        public virtual User User { get; set; }
    }
}
