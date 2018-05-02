namespace CvSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Article
    {
        [Key]
        public int ar_id { get; set; }

        [StringLength(50)]
        public string ar_baslik { get; set; }

        [StringLength(250)]
        public string ar_icerik { get; set; }

        [StringLength(150)]
        public string ar_resim { get; set; }

        public int? ar_cat_id { get; set; }

        public int? ar_user_id { get; set; }

        public virtual Category Category { get; set; }

        public virtual User User { get; set; }
    }
}
