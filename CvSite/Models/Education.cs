namespace CvSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Education")]
    public partial class Education
    {
        [Key]
        public int edu_id { get; set; }

        [StringLength(150)]
        public string edu_okul_adi { get; set; }

        [StringLength(50)]
        public string edu_bolum { get; set; }

        [StringLength(50)]
        public string edu_giris_tarihi { get; set; }

        [StringLength(50)]
        public string edu_cikis_tarihi { get; set; }
    }
}
