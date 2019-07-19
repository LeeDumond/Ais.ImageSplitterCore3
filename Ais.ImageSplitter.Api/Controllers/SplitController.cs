using System.Net;
using System.Threading.Tasks;
using Ais.ImageSplitter.Wpf;
using Microsoft.AspNetCore.Authorization;
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
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return result;
        }
    }
}