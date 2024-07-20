using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace EffectiveWebProg.Controllers;

public class BaseController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
        var session_SSID = HttpContext.Session.GetString("SSID");
        var session_user_email = HttpContext.Session.GetString("SSName");
        if (session_user_email == null)
        {
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }
        else
        {
            ViewBag.UserType = HttpContext.Session.GetString("SSUserType");
            ViewBag.SSID = session_SSID;
        }
    }

    new public ContentResult Forbid()
    {
        Response.StatusCode = 403;
        return Content("Forbidden: You are not authorised to access this resource.");
    }

    new public ContentResult NotFound()
    {
        Response.StatusCode = 404;
        return Content("Not Found: This resource does not exist.");
    }

}
