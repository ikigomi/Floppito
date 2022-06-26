using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using solarLab.Contracts.Errors;
using solarLab.Contracts.Validators;
using solarLab.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace solarLab.Api.Middlewares
{
    /// <summary>
    /// Middleware для перехвата исключений
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;


        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (DomainException e)
            {
                Log.Error(e, e.ToString());

                var result = SerializeMessages(e.Message);

                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                await context.Response.WriteAsync(result);
            }

            catch (Exception e)
            {
                Log.Error(e, e.ToString());

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var result = SerializeMessages("Произошла ошибка сервера");

                await context.Response.WriteAsync(result);
            }
        }


        private string SerializeMessages(string message)
        {
            var errorResponse = new ErrorsResponse();

            var errors = message.Split('\n');
            foreach (var error in errors)
            {
                var errorModel = new Error
                {
                    Message = error
                };

                errorResponse.Errors.Add(errorModel);
            }



            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var result = JsonSerializer.Serialize(errorResponse, options);
            return result;
        }
    }
}
