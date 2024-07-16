using BarberShopApi.Application.Exceptions;
using BarberShopApi.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BarberShopApi.Filters
{
    public class BarberExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is BarberException)
            {
                var barberException = (BarberException)context.Exception;   
                context.HttpContext.Response.StatusCode = (int)barberException.GetStatusCode();

                var erroJson = new ErroJson { Errors = barberException.GetErrorMessages()};

                var response = new Response<ErroJson>(erroJson, (int)barberException.GetStatusCode());

                context.Result = new ObjectResult(response);
            }
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var errors = new List<string> { "internal server error"};    

                var erroJson = new ErroJson { Errors = errors };

                context.Result = new ObjectResult(new Response<ErroJson>(erroJson, 500));
            }
            
        }
    }
}
