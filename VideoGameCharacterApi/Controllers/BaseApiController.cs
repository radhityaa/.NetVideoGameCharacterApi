using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoGameCharacterApi.DTOs;

namespace VideoGameCharacterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected ActionResult<ServiceResponse<T>> SuccessResponse<T>(T? data = default, string message = "Success")
        {
            var response = new ServiceResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };

            return Ok(response);
        }

        protected ActionResult<ServiceResponse<T>> ErrorResponse<T>(string message, int statusCode = 404)
        {
            var response = new ServiceResponse<T>
            {
                Success = false,
                Message = message,
                Data = default
            };

            return statusCode switch
            {
                400 => BadRequest(response),
                404 => NotFound(response),
                _ => StatusCode(statusCode, response)
            };
        }
    }
}
