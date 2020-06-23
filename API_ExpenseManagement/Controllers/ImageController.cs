using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ExpenseManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnviroment;
        public ImageController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnviroment = hostingEnvironment;
        }

        //GET api/image/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var path = Path.Combine(_hostingEnviroment.WebRootPath, "image", $"{id}");
            var imageFileStream = System.IO.File.OpenRead(path);
            return File(imageFileStream, "Image/png");
        }
    }
}