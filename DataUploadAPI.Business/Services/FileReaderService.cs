using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DataUploadAPI.Business.Contracts;
using Microsoft.AspNetCore.WebUtilities;

namespace DataUploadAPI.Business.Services
{
    public class FileReaderService
    {
        private readonly Stream _stream;
        private readonly IDataImporterService _importerService;
        private readonly IMultiPartStreamReaderService _multiPartStreamReaderService;
        public FileReaderService(IDataImporterService dataImporterService,IMultiPartStreamReaderService multiPartStreamReaderService,Stream stream)
        {
            _multiPartStreamReaderService = multiPartStreamReaderService;
            _stream = stream;
            _importerService = dataImporterService;
        }

        public async Task<int> ReadFileAsync(CancellationToken ct = default)
        {
            var list = await _multiPartStreamReaderService.Parse(_stream,ct);
            await _importerService.SaveAllProductsAsync(list,ct);
            return list.Count;
        }
    }
}