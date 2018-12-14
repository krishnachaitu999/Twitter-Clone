namespace TwitterClone.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TWEET")]
    public partial class TWEET
    {
        [Key]
        [Column(Order = 0)]
        public int Tweet_ID { get; set; }

        
        [StringLength(25)]
        public string User_ID { get; set; }

        
        public string Message { get; set; }

        
        public DateTime Created { get; set; }

       // public virtual PERSON PERSON { get; set; }
    }
}
