using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Infrastructure
{
    //Custom action filter - https://docs.microsoft.com/en-us/previous-versions/aspnet/dd381609(v=vs.100)

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SelectedTabAttribute : ActionFilterAttribute
    {
        private readonly string _selectedTab;

        public SelectedTabAttribute(string selectedTab)
        {
            _selectedTab = selectedTab;
        }

        /*
        Called by the ASP.NET MVC framework before the action result executes.
        
        The OnResultExecuting method is called just before the ActionResult 
        instance that is returned by your action is invoked.
        */
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.selectedTab = _selectedTab;
        }
    }
}