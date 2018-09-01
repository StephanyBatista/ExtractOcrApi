using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExtractOcrApi.Controllers
{
    [Route("/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public string Get()
        {
            return 
                @"OCR is working, please call 
                GET /ocr?url=url_of_document&type=type_of_document(pdf, docx, pgn, jpeg, jpg) to extract OCR";
        }
    }
}
