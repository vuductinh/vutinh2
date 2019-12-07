namespace Shop_Shoes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order_s
    {
        [Key]
        public int orderID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreateDate { get; set; }

        public int? CustomerID { get; set; }

        [Required]
        [StringLength(200)]
        public string ShipName { get; set; }

        [Required]
        [StringLength(200)]
        public string Shipmobile { get; set; }

        [Required]
        [StringLength(200)]
        public string ShipAdress { get; set; }

        [Required]
        [StringLength(200)]
        public string ShipEmail { get; set; }

        public bool? Status_ { get; set; }

        public int? CustumerID { get; set; }
    }
}
