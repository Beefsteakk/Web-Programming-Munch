using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace EffectiveWebProg.Controllers;

public class BaseController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
        var session_user_email = HttpContext.Session.GetString("SSName");
        if (session_user_email == null)
        {
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }
}
