namespace CvSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Project
    {
        [Key]
        public int pro_id { get; set; }

        [StringLength(50)]
        public string pro_ad { get; set; }

        [StringLength(250)]
        public string pro_tanitim { get; set; }

        [StringLength(250)]
        public string pro_resim { get; set; }

        [StringLength(50)]
        public string pro_kullanilan_kutuphane { get; set; }

        [StringLength(50)]
        public string pro_tarih { get; set; }
    }
}
