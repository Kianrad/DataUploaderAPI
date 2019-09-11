using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataUploadAPI.Business.Contracts
{
    public interface IJsonReaderWriterService
    {
        Task WriteString(string fileName, string jsonString);
        Task WriteObject(string fileName, IEnumerable<dynamic> jsonObject);
    }
}