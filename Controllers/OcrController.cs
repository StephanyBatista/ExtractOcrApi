using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ExtractOcrApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OcrController : ControllerBase
    {
        private readonly IHostingEnvironment _environment;
        private readonly FileHelper _fileHelper;
        private readonly DocumentToText _documentToText;

        public OcrController(IHostingEnvironment environment, FileHelper fileHelper, DocumentToText documentToText)
        {
            _environment = environment;
            _fileHelper = fileHelper;
            _documentToText = documentToText;
        }
        
        [HttpGet]
        public async Task<dynamic> Get()
        {
            var url = Request.Query["url"];
            var type = Request.Query["type"];
            
            if(string.IsNullOrEmpty(url) || string.IsNullOrEmpty(type))
                return new {Error =  "Params invalid. Url and Type must be send"};

            var result = await _fileHelper.GetAndSaveFile(url, type, _environment.ContentRootPath);
            if(!result.Success) return new { Error = result.Error }; 

            var textExtracted = await _documentToText.Extract(type, result.FilePath); //await ExtractOcr(type, result.FilePath);
            _fileHelper.DeleteFile(result.FilePath);

            return new { Text = textExtracted };
        }
    }
}
