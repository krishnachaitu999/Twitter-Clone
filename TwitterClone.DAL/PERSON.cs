namespace TwitterClone.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PERSON")]
    public partial class PERSON
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PERSON()
        {
            this.TWEETs = new HashSet<TWEET>();
            this.Following = new HashSet<PERSON>();
            this.Followers = new HashSet<PERSON>();
        }

        [Key]
        public string User_ID { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public System.DateTime Joined { get; set; }
        public bool Active { get; set; }

        //public virtual int FollowingCount { get; set; }
       // public virtual int FollwersCount { get; set; }

        public virtual ICollection<TWEET> TWEETs { get; set; }
        public virtual ICollection<PERSON> Following { get; set; }
        public virtual ICollection<PERSON> Followers { get; set; }
    }
}
