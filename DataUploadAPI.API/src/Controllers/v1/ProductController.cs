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
        public async Task<ActionResult> Upload(CancellationToken ct)
        {

            var context = HttpContext;
            if (!Helpers.MultiPartFileHelper.IsMultipartContentType(context.Request.ContentType))
            {
                return NotFound();
            }

            FileReaderService fileReaderService = new FileReaderService(_importerService,_multiPartStreamReaderService,context.Request.Body);
            var result = await fileReaderService.ReadFileAsync(ct);

            var resultObj = new ResultResponse()
            {
                ParsedRows = result
            };

            return Ok(resultObj);
        }
        
        
        
        

      
    }
}