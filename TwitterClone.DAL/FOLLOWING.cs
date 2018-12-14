namespace TwitterClone.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FOLLOWING")]
    public partial class FOLLOWING
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(25)]
        public string User_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(25)]
        public string Following_ID { get; set; }
    }
}
