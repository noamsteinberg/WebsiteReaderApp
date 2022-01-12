using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteReaderApp.Models;

namespace WebsiteReaderApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebReaderController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get5Images(string Topic)
        {
            GoogleWebsiteReader websiteReader = new GoogleWebsiteReader(Topic);

            return Ok(websiteReader.Download5TopPhotos());
        }
    }
}
