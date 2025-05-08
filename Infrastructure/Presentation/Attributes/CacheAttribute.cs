using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;

namespace Presentation.Attributes
{
    public class CacheAttribute(int sec) : Attribute, IAsyncActionFilter
    {
        
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CahceService;
            var cacheKey = GenerateCacheKey(context.HttpContext.Request);
            var result = await cacheService.GetCacheValueAsync(cacheKey);
            if (!string.IsNullOrEmpty(result)) 
            {
                // Return Response
                context.Result = new ContentResult()
                { 
                    ContentType = "application/json",
                    StatusCode = 200,
                    Content = result
                };
                return;
            }
            // Execute The Endpoint
            var contextResult = await next.Invoke();
            if(contextResult.Result is OkObjectResult okObject)
            {
                await cacheService.SetCahceValueAsync(cacheKey, okObject.Value, TimeSpan.FromSeconds(sec));
            }
        }

        private string GenerateCacheKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path);
            foreach (var item in request.Query.OrderBy(q => q.Key))
            {
                key.Append($"|{item.Key}-{item.Value}");
            }
            return key.ToString();
        }
    }
}
