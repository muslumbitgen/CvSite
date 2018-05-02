namespace CvSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Skill
    {
        [Key]
        public int skill_id { get; set; }

        [StringLength(50)]
        public string skill_ad { get; set; }

        public int? skill_oran { get; set; }
    }
}
