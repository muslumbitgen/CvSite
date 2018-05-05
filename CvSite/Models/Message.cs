namespace CvSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Message")]
    public partial class Message
    {
        [Key]
        public int mes_id { get; set; }

        [StringLength(50)]
        public string mes_adi { get; set; }

        [StringLength(50)]
        public string mes_email { get; set; }

        [StringLength(50)]
        public string mes_konu { get; set; }

        [StringLength(250)]
        public string mes_mesaj { get; set; }

        public DateTime? mes_tarihi { get; set; }

        public int? uye_id { get; set; }
    }
}
