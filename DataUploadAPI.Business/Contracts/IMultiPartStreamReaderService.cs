using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DataUploadAPI.Business.ApiModels;
using DataUploadAPI.Data.Entities;
using Microsoft.AspNetCore.WebUtilities;

namespace DataUploadAPI.Business.Contracts
{
    public interface IMultiPartStreamReaderService
    {
        Task<List<ProductApiModel>> Parse(Stream stream, CancellationToken ct = default);
        ProductApiModel ProductParser(string source);
    }
}