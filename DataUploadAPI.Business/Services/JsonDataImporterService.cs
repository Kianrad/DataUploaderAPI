using System;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataUploadAPI.Business.ApiModels;
using DataUploadAPI.Business.Contracts;
using DataUploadAPI.Data.Contracts;
using DataUploadAPI.Data.Entities;

namespace DataUploadAPI.Business.Services
{
     public class JsonDataImporterService : IDataImportHandler
     {

         private readonly IJsonReaderWriterService _jsonReaderWriterService;
        public JsonDataImporterService(IJsonReaderWriterService jsonReaderWriterService)
        {
            _jsonReaderWriterService = jsonReaderWriterService;
        }

        public async Task SaveAllAsync(IEnumerable<ProductApiModel> products, CancellationToken ct = default)
        {
            string filename = Guid.NewGuid().ToString();
            filename += ".json";
            await _jsonReaderWriterService.WriteObject(filename,products);
        } 

        
    }
}