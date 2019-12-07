namespace Shop_Shoes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        public int? productID { get; set; }

        public int? OrderID { get; set; }

        public int? quantity { get; set; }

        public int? price { get; set; }

        public int ID { get; set; }
    }
}
