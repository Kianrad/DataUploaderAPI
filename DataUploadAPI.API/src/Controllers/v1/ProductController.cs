using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DataUploadAPI.API.CustomAttributes;
using DataUploadAPI.API.Models;
using DataUploadAPI.Business.Contracts;
using DataUploadAPI.Business.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace DataUploadAPI.API.Controllers.v1
{
    
    [Route("api/v1/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class ProductController : Controller
    {
        
        private readonly IDataImporterService _importerService;
        private readonly IMultiPartStreamReaderService _multiPartStreamReaderService;

        public ProductController(IDataImporterService importerService,IMultiPartStreamReaderService multiPartStreamReaderService)
        {
            _multiPartStreamReaderService = multiPartStreamReaderService;
            _importerService = importerService;
        }

        [HttpPost]
        [DisableFormValueModelBinding]
        [IsMultiPartContent]
        public async Task<ActionResult<ResultResponse>> Upload(CancellationToken ct)
        {
            var context = HttpContext;
            FileReaderService fileReaderService = new FileReaderService(_importerService,_multiPartStreamReaderService,context.Request.Body);
            return new ResultResponse()
            {
                ParsedRows = await fileReaderService.ReadFileAsync(ct)
            };
        }
        
        
        
        

      
    }
}