using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace web.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is DbUpdateConcurrencyException)
            {
                context.Result = new ConflictObjectResult(new { Message = "Entity was updated in between, please refresch your copy." });
            }
        }
    }
}
