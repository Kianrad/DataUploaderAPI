using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using DataUploadAPI.Business.Contracts;
using Newtonsoft.Json;

namespace DataUploadAPI.Business.Services
{
    public class JsonReaderWriterService : IJsonReaderWriterService
    {
        private string _path { set; get; }

        public JsonReaderWriterService(string path)
        {
            _path = path;
        }
 
        public async Task WriteString(string fileName, string jsonString)
        {

            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);
            
            var path = Path.Combine(
                _path,fileName);
 
            using (var streamWriter = File.CreateText(path))
            {
                await streamWriter.WriteAsync(jsonString);
            }
        }
        
        public async Task WriteObject(string fileName, IEnumerable<dynamic> jsonObject)
        {
            string jsonString = JsonConvert.SerializeObject(jsonObject);
            await WriteString(fileName, jsonString);
        }
        
    }
    
}