using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataUploadAPI.Business.ApiModels;
using DataUploadAPI.Business.Contracts;


namespace DataUploadAPI.Business.Services
{
    public class MultiPartFileStreamReaderService : IMultiPartStreamReaderService
    {

        private StringBuilder _errors;
        public MultiPartFileStreamReaderService()
        {
            _errors = new StringBuilder();    
        }
        
        public async Task<List<ProductApiModel>> Parse(Stream stream, CancellationToken ct = default)
        {
            int lineCounter = 0;
            List<ProductApiModel> products = new List<ProductApiModel>();
            using(var streamReader = new StreamReader(stream)) {
                string line;
                while ((line = await streamReader.ReadLineAsync()) != null)
                {
                    lineCounter++;
                    if (lineCounter == 1) continue;
                    ProductApiModel productApiModels =
                         ProductParser(line);
                    if (productApiModels != null)
                        products.Add(productApiModels);
                }
            }
            return products;

        }
        
        
        public ProductApiModel ProductParser(string source)
        {
            ProductApiModel res = null;
            var columns = source.Split(',');
                try
                {
                    res = new ProductApiModel()
                    {
                        Key = columns[0],
                        CategoryId = columns[1],
                        Category = new CategoryApiModel()
                        {
                            ColorCode = columns[2],
                            Description = columns[3],
                            Id = columns[1]
                        },
                        Color = columns[9],
                        Price = Convert.ToDecimal(columns[4]),
                        DiscountPrice = Convert.ToInt16(columns[5]),
                        DeliveredIn = columns[6],
                        Q1 = columns[7],
                        Size = Convert.ToInt16(columns[8])
                    };
                }
                catch
                {
                    _errors.Append(source);
                }
            return res;
        } 
    }
}