using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataUploadAPI.Data.Entities;
using Newtonsoft.Json;

namespace DataUploadAPI.Business.ApiModels
{
    public class CategoryApiModel 
    {
        [Key]
        public string Id { get; set; }
        
        public string ColorCode { get; set; }

        public string Description { get; set; }

    }
}