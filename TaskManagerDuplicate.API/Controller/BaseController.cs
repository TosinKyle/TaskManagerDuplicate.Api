using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerDuplicate.Domain.SharedModels;

namespace TaskManagerDuplicate.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IActionResult BuildHttpResponse<T>(BaseApiResponse<T> requestResponse)//is the http resp from the status code in the service
        {
            return requestResponse.ResponseCode switch
            {
                StatusCodes.Status200OK => Ok(requestResponse),
                StatusCodes.Status201Created => Created("", requestResponse),
                StatusCodes.Status409Conflict => BadRequest(requestResponse),
                StatusCodes.Status403Forbidden => BadRequest(requestResponse),
                StatusCodes.Status404NotFound => NotFound(requestResponse),
                StatusCodes.Status401Unauthorized => Unauthorized(requestResponse),
                StatusCodes.Status400BadRequest => BadRequest(requestResponse),
                StatusCodes.Status413PayloadTooLarge => BadRequest(requestResponse),
                StatusCodes.Status415UnsupportedMediaType => BadRequest(requestResponse),
                StatusCodes.Status500InternalServerError => StatusCode(requestResponse.ResponseCode),
                StatusCodes.Status204NoContent => NoContent(),
            };
        }
    }
}
