using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace onboardingAPI.Middlewares
{
    public class VerifiedMiddleware
    {
        private readonly RequestDelegate _next;

        public VerifiedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the endpoint has the Authorize attribute
            var endpoint = context.GetEndpoint();
            if (endpoint != null && endpoint.Metadata.GetMetadata<AuthorizeAttribute>() != null)
            {
                // Check if the user is authenticated
                if (context.User.Identity.IsAuthenticated)
                {
                    // Get the 'Verified' claim or retrieve the user from the database to check 'Verified'
                    var verifiedClaim = context.User.Identity;
                    Console.WriteLine("hi, john doe", verifiedClaim);
                    if (verifiedClaim == null)
                    {
                        // User is not verified, return 403 Forbidden
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        await context.Response.WriteAsync("User is not verified.");
                        return;
                    }
                }Console.WriteLine("hi, john doewdwe");
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}