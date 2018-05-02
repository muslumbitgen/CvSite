namespace CvSite.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CVSiteDB : DbContext
    {
        public CVSiteDB()
            : base("name=CVSiteDB")
        {
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<Experience> Experiences { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<Social> Socials { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Articles)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.ar_cat_id);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Articles)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.ar_user_id);
        }
    }
}
