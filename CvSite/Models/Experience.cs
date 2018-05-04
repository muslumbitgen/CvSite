namespace CvSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Experience")]
    public partial class Experience
    {
        [Key]
        public int expe_id { get; set; }

        [StringLength(50)]
        public string expe_ad { get; set; }

        [StringLength(50)]
        public string expe_link { get; set; }

        [StringLength(50)]
        public string expe_pozisyon { get; set; }

        [StringLength(50)]
        public string expe_konum { get; set; }

        [StringLength(50)]
        public string expe_giris_tarihi { get; set; }

        [StringLength(50)]
        public string expe_cikis_tarihi { get; set; }
    }
}
