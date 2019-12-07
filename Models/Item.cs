namespace Shop_Shoes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Item
    {
        [Key]
        public int IdItem { get; set; }

        [Required]
        [StringLength(200)]
        public string Name_Item { get; set; }

        [Required]
        [StringLength(200)]
        public string Made_in { get; set; }

        public int Size { get; set; }

        [Required]
        [StringLength(20)]
        public string Color { get; set; }

        public int Amount { get; set; }

        public int? Id_Type { get; set; }

        public int? cost { get; set; }

        [Column(TypeName = "ntext")]
        public string linkImg { get; set; }

        public int? IdStyle { get; set; }

        public virtual Type_s Type_s { get; set; }

        public virtual Style Style { get; set; }
    }
}
