using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataUploadAPI.Data.Entities;
using Newtonsoft.Json;

namespace DataUploadAPI.Business.ApiModels
{
    public class ProductApiModel 
    {
        [Key]
        public string Key { get; set; }
        
        public string CategoryId { set; get; }
        
        public CategoryApiModel Category { get; set; }
        
        public decimal Price { get; set; }

        public decimal DiscountPrice { get; set; }
        
        public string DeliveredIn { set; get; }
        
        public string Q1 { set; get; }
        
        public short Size { set; get; }
        
        public string Color { set; get; }

    }
}