using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ais.ImageSplitter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            string version = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                .InformationalVersion;

            return $"Ais.ImageSplitter.Api v{version}";
        }
    }
}