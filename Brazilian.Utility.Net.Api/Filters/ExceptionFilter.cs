using FluentValidation;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Brazilian.Utility.Net.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public ExceptionFilter()
        {
        }
        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            if(context.Exception is FlurlHttpException flurlException)
            {
                context.HttpContext.Response.StatusCode = (int)flurlException.StatusCode;
                context.Result = new ObjectResult(flurlException.StatusCode.ToString())
                {
                    StatusCode = (int)flurlException.StatusCode
                };

            }
            else if(context.Exception is ValidationException validationException)
            {
                var errors = validationException.Errors
                    .Select(error => error.ErrorMessage)
                    .ToHashSet()
                    .ToArray();

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new ObjectResult(ErrorResponse.Create(errors))
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Result = new ObjectResult(ErrorResponse.Create(context.Exception.Message))
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
        public class ErrorResponse
        {
            public List<string> Errors { get; set; }

            public ErrorResponse()
            {
                Errors = new List<string>();
            }
            public static ErrorResponse Create(params string[] errors)
            {
                var response = new ErrorResponse();

                response.Errors.AddRange(errors);

                return response;
            }

        }

    }
}
