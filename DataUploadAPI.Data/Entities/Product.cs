using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace DataUploadAPI.Data.Entities
{
    public class Product
    {
        [Key]
        public string Key { get; set; }
        
        public string CategoryId { set; get; }
        
        [NotMapped]
        public virtual Category Category { get; set; }
        
        public decimal Price { get; set; }

        public decimal DiscountPrice { get; set; }
        
        public string DeliveredIn { set; get; }
        
        public string Q1 { set; get; }
        
        public Int16 Size { set; get; }
        
        public string Color { set; get; }
        
        
    }
}