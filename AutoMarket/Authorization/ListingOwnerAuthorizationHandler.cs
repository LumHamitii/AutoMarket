using AutoMarket.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AutoMarket.Authorization
{
    public class ListingOwnerAuthorizationHandler : AuthorizationHandler<ListingOwnerRequirement, object>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ListingOwnerRequirement requirement, object resource)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = context.User.IsInRole("Admin");
            var isOwner = false;

            if (resource is Car car)
            {
                isOwner = car.User != null && car.User.Id == userId;
            }
            else if (resource is Motorcycle motorcycle)
            {
                isOwner = motorcycle.User != null && motorcycle.User.Id == userId;
            }
            if (isOwner || isAdmin)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
