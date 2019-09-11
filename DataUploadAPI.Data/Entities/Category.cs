using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataUploadAPI.Data.Entities
{
    public class Category 
    {
      
        [Key]
        public string Id { get; set; }
        
        public string ColorCode { get; set; }

        public string Description { get; set; }
        
        [NotMapped]
        public virtual List<Product> Products { set; get; }

    }
}