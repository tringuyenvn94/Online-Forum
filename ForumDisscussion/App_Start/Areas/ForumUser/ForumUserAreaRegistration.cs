using System.Web.Mvc;

namespace ForumDisscussion.Areas.ForumUser
{
    public class ForumUserAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ForumUser";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ForumUser_default",
                "ForumUser/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
