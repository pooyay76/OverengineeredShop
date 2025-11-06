using Auth.Api.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Auth.Api.Controllers.ActionFilters
{
    public class AccessControlActionFilterAttribute : ActionFilterAttribute
    {
        public string Permission { get; set; }


        public override void OnActionExecuting(ActionExecutingContext context)
        {

            IPermissionService permissionService = context.HttpContext.RequestServices.GetService<IPermissionService>();
            var userId = Guid.Parse(context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);


            if (permissionService.CheckPermission(userId, Permission))
            {
                context.Result = new BadRequestObjectResult("Not authorized!");
            }

            else
            {
                base.OnActionExecuting(context);
            }

        }
    }
}
