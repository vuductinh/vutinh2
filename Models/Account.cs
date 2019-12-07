namespace Shop_Shoes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [Key]
        public int idAccount { get; set; }

        [StringLength(200)]
        public string User_s { get; set; }

        [StringLength(200)]
        public string Pass_word { get; set; }
    }
}
