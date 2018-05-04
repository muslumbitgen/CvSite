namespace CvSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Articles = new HashSet<Article>();
            Resumes = new HashSet<Resume>();
        }

        [Key]
        public int user_id { get; set; }

        [StringLength(50)]
        public string userAd { get; set; }

        [StringLength(50)]
        public string userSoyad { get; set; }

        [StringLength(50)]
        public string userEmail { get; set; }

        [StringLength(50)]
        public string userTelefon { get; set; }

        [StringLength(50)]
        public string userKulAdi { get; set; }

        [StringLength(50)]
        public string userSifre { get; set; }

        [StringLength(50)]
        public string userRole { get; set; }

        public bool? userActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Article> Articles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Resume> Resumes { get; set; }
    }
}
