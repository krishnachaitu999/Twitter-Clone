namespace TwitterClone.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TwitterCloneDataContext : DbContext
    {
        public TwitterCloneDataContext()
            : base("name=TwitterCloneConnection")
        {
        }

        public virtual DbSet<PERSON> PERSON { get; set; }
        public virtual DbSet<TWEET> TWEET { get; set; }
        public virtual DbSet<FOLLOWING> FOLLOWER{ get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PERSON>()
                .Property(e => e.User_ID)
                .IsUnicode(false);

            modelBuilder.Entity<PERSON>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<PERSON>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<PERSON>()
                .Property(e => e.Email)
                .IsUnicode(false);

            //modelBuilder.Entity<PERSON>()
            //    .HasMany(e => e.TWEETs)
            //    .WithRequired(e => e.PERSON)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<FOLLOWING>()
            //    .HasMany(e => e.User_ID)
            //    .WithMany(e => e.Followers)
            //    .Map(m => m.ToTable("FOLLOWING").MapLeftKey("User_ID").MapRightKey("Following_ID"));
            modelBuilder.Entity<FOLLOWING>()
               .Property(e => e.User_ID)
               .IsUnicode(false);

            modelBuilder.Entity<FOLLOWING>()
              .Property(e => e.Following_ID)
              .IsUnicode(false);

            //modelBuilder.Entity<TWEET>()
            //    .Property(e => e.User_ID)
            //    .IsUnicode(false);

            //modelBuilder.Entity<TWEET>()
            //    .Property(e => e.Message)
            //    .IsUnicode(false);
        }
    }
}
