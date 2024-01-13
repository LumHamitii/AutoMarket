using AutoMarket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace AutoMarket.Authorization
{
    public class ListingOwnerAuthorizationHandler : AuthorizationHandler<ListingOwnerRequirement, Car>
    {
       

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ListingOwnerRequirement requirement, Car car)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = context.User.IsInRole("Admin");
            var isOwner = car.User != null && car.User.Id == userId;


            if (isOwner || isAdmin)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
