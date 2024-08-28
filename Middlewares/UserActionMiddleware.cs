using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Authorization;
using onboardingAPI.Data;
using onboardingAPI.Models;

namespace onboardingAPI.Middlewares
{
    public class UserActionMiddleware
    {
        private readonly RequestDelegate _next;
        public UserActionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint != null && endpoint.Metadata.GetMetadata<AuthorizeAttribute>() != null)
            {
                // Check if the user is authenticated
                if (context.User.Identity.IsAuthenticated)
                {
                    Console.WriteLine("request sent login");
                }
                else
                {
                    Console.WriteLine("request sent unlogin");
                }
            }

            await _next(context);
        } 
    }
}