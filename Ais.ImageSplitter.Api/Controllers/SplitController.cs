using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ais.ImageSplitter.Wpf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ais.ImageSplitter.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SplitController : ControllerBase
    {
        private readonly ISplitter _splitter;

        public SplitController(ISplitter splitter)
        {
            _splitter = splitter;
        }

        [HttpPost]
        public async Task<ActionResult<SplitResult>> Post(SplitRequest request)
        {
            SplitResult result = await _splitter.SplitAsync(request.InputFilePath, request.OutputFilePath, request.PageList);

            if (result.ErrorStatus != null)
            {
                var statusCode = (int)HttpStatusCode.InternalServerError;
                Response.StatusCode = statusCode;
            }

            return result;
        }
    }
}