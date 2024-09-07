using Microsoft.AspNetCore.Mvc;
using Presentation.ResponseSchema;
using System.Net;

namespace webapi._Base
{
    public class CustomJsonResponse<T1> : ControllerBase
    {
        private SuccessResponse SuccessResponse = new SuccessResponse();
        private ErrorResponse ErrorResponse = new ErrorResponse();
        private FailedAuthResponse FailedAuthResponse = new FailedAuthResponse();

        /// <summary>
        /// If object successfully created then controller send 201 created response status other bad request 401
        /// </summary>
        /// <param name="paras">Object format</param>
        /// <returns>return 201 created response</returns>
        public IActionResult CREATE(IdenticalServiceResponse<T1> paras)
        {
            if (paras.Successed)
            {
                SuccessResponse.SuccessMessage = ResponseMessage.Created.ToString();
                SuccessResponse.StatusCode = (int)HttpStatusCode.Created;
                SuccessResponse.Result = paras.Result;
                return Created("", SuccessResponse);
            }
            else
            {
                ErrorResponse.SuccessMessage = ResponseMessage.Redundancy.ToString();
                ErrorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                ErrorResponse.Errors = new List<string>();
                ErrorResponse.Errors.Add(paras.Errors!);
                return BadRequest(ErrorResponse);
            }
        }

        /// <summary>
        /// If object operation successfully done then controller send 200 success response status other bad request 401
        /// </summary>
        /// <param name="paras">Object format</param>
        /// <returns>return 200 success response</returns>
        public IActionResult UPDATE(IdenticalServiceResponse<T1> paras)
        {
            if (paras.Successed)
            {
                SuccessResponse.SuccessMessage = ResponseMessage.Success.ToString();
                SuccessResponse.StatusCode = (int)HttpStatusCode.OK;
                SuccessResponse.Result = paras.Result;
                return Ok(SuccessResponse);
            }
            else
            {
                ErrorResponse.SuccessMessage = ResponseMessage.BadRequest.ToString();
                ErrorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                ErrorResponse.Errors = new List<string>();
                ErrorResponse.Errors.Add(paras.Errors!);
                return BadRequest(ErrorResponse);
            }
        }

        /// <summary>
        /// If object operation successfully done then controller send 200 success response status other bad request 401
        /// </summary>
        /// <param name="paras">Object format</param>
        /// <returns>return 200 success response</returns>
        public IActionResult DELETE(IdenticalServiceResponse<T1> paras)
        {
            if (paras.Successed)
            {
                SuccessResponse.SuccessMessage = ResponseMessage.DeleteSuccessfully.ToString();
                SuccessResponse.StatusCode = (int)HttpStatusCode.OK;
                SuccessResponse.Result = paras.Result;
                return Ok(SuccessResponse);
            }
            else
            {
                ErrorResponse.SuccessMessage = ResponseMessage.BadRequest.ToString();
                ErrorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                ErrorResponse.Errors = new List<string>();
                ErrorResponse.Errors.Add(paras.Errors!);
                return BadRequest(ErrorResponse);
            }
        }

        /// <summary>
        /// If object operation successfully done then controller send 200 success response status other bad request 401
        /// </summary>
        /// <param name="paras">Object format</param>
        /// <returns>return 200 success response</returns>
        public IActionResult GET(IdenticalServiceResponse<T1> paras)
        {
            if (paras.Successed)
            {
                SuccessResponse.SuccessMessage = ResponseMessage.Success.ToString();
                SuccessResponse.StatusCode = (int)HttpStatusCode.OK;
                SuccessResponse.Result = paras.Result;
                return Ok(SuccessResponse);
            }
            else
            {
                ErrorResponse.SuccessMessage = ResponseMessage.DataNotFound.ToString();
                ErrorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                ErrorResponse.Errors = new List<string>();
                ErrorResponse.Errors.Add(paras.Errors!);
                return NotFound(ErrorResponse);
            }
        }

        /// <summary>
        /// If object operation successfully done then controller send 200 success response status other bad request 401
        /// </summary>
        /// <param name="paras">Object format</param>
        /// <returns>return 200 success response</returns>
        public IActionResult GET(IdenticalServiceResponse<List<T1>> paras)
        {
            if (paras.Successed)
            {
                SuccessResponse.SuccessMessage = ResponseMessage.Success.ToString();
                SuccessResponse.StatusCode = (int)HttpStatusCode.OK;
                SuccessResponse.Result = paras.Result;
                return Ok(SuccessResponse);
            }
            else
            {
                ErrorResponse.SuccessMessage = ResponseMessage.DataNotFound.ToString();
                ErrorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                ErrorResponse.Errors = new List<string>();
                ErrorResponse.Errors.Add(paras.Errors!);
                return NotFound(ErrorResponse);
            }
        }

        /// <summary>
        /// If object operation successfully done then controller send 200 success response status other bad request 401
        /// </summary>
        /// <param name="paras">Object format</param>
        /// <returns>return 200 success response</returns>
        public IActionResult SUCCESS(IdenticalServiceResponse<T1> paras)
        {
            if (paras.Successed)
            {
                SuccessResponse.SuccessMessage = ResponseMessage.Success.ToString();
                SuccessResponse.StatusCode = (int)HttpStatusCode.OK;
                SuccessResponse.Result = paras.Result;
                return Ok(SuccessResponse);
            }
            else
            {
                ErrorResponse.SuccessMessage = ResponseMessage.DataNotFound.ToString();
                ErrorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                ErrorResponse.Errors = new List<string>();
                ErrorResponse.Errors.Add(paras.Errors!);
                return NotFound(ErrorResponse);
            }
        }

        /// <summary>
        /// If object operation successfully done then controller send 200 success response status other bad request 401
        /// </summary>
        /// <param name="paras">Object format</param>
        /// <returns>return 203 auth response</returns>
        public IActionResult AUTHENTICATE(IdenticalServiceResponse<AuthResponse> paras)
        {
            if (paras.Successed)
            {
                paras!.Result!.SuccessMessage = ResponseMessage.Authenticate.ToString();
                paras.Result.StatusCode = (int)HttpStatusCode.OK;
                return Ok(paras.Result);
            }
            else
            {
                FailedAuthResponse.SuccessMessage = ResponseMessage.Unauthenticate.ToString();
                FailedAuthResponse.StatusCode = (int)HttpStatusCode.Unauthorized;
                FailedAuthResponse.Errors = new List<string>();
                FailedAuthResponse.Errors.Add(paras.Errors!);
                FailedAuthResponse.IsAuthenticate = false;
                return Unauthorized(FailedAuthResponse);
            }
        }
    }
}
