namespace CvSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Social
    {
        [Key]
        public int sos_id { get; set; }

        [StringLength(50)]
        public string sos_adi { get; set; }

        [StringLength(50)]
        public string sos_Class { get; set; }

        [StringLength(50)]
        public string sos_Url { get; set; }
    }
}
