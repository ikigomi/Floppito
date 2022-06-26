using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using solarLab.Contracts.Errors;
using solarLab.Contracts.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Infrastructure.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                // если нужно поле с ошибкой errorsInModelState[i].key
                //var errorsInModelState = context.ModelState
                //    .Where(x => x.Value.Errors.Count > 0)
                //    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage))
                //    .ToArray();

                var errorsInModelState = context.ModelState.Where(x => x.Value.Errors.Count > 0).Select(_ => _.Value.Errors.Select(_ => _.ErrorMessage));


                var errorResponse = new ErrorsResponse();

                foreach (var errors in errorsInModelState)
                {
                    foreach (var error in errors)
                    {
                        var errorModel = new Error
                        {
                            Message = error
                        };

                        errorResponse.Errors.Add(errorModel);
                    }
                }

                context.Result = new BadRequestObjectResult(errorResponse);

                return;

            }

            await next();
        }
    }
}
