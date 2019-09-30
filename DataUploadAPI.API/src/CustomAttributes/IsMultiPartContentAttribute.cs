using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DataUploadAPI.API.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class IsMultiPartContentAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (!Helpers.MultiPartFileHelper.IsMultipartContentType(context.HttpContext.Request.ContentType))
            {
                context.HttpContext.Response.StatusCode = 404;
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }
    }
}