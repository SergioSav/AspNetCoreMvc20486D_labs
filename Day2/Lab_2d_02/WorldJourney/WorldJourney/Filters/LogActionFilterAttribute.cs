using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace WorldJourney.Filters
{
    public class LogActionFilterAttribute : ActionFilterAttribute
    {
        private IHostingEnvironment _environment;
        private string _contentRootPath;
        private string _logPath;
        private string _filename;
        private string _fullPath;

        public LogActionFilterAttribute(IHostingEnvironment environment)
        {
            _environment = environment;
            _contentRootPath = environment.ContentRootPath;
            _logPath = _contentRootPath + "/LogFile/";
            _filename = $"log{DateTime.Now.ToString("dd-MM-yy-H-mm")}.txt";
            _fullPath = _logPath + _filename;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Directory.CreateDirectory(_logPath);
            var actionName = filterContext.ActionDescriptor.RouteValues["action"];
            var controllerName = filterContext.ActionDescriptor.RouteValues["controller"];
            using (FileStream fs = new FileStream(_fullPath, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine($"The action {actionName} in {controllerName} controller started, event fired OnActionExecuting");
                }
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var actionName = filterContext.ActionDescriptor.RouteValues["action"];
            var controllerName = filterContext.ActionDescriptor.RouteValues["controller"];
            using (FileStream fs = new FileStream(_fullPath, FileMode.Append))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine($"The action {actionName} in {controllerName} controller finished, event fired OnActionExecuted");
                }
            }
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var actionName = filterContext.ActionDescriptor.RouteValues["action"];
            var controllerName = filterContext.ActionDescriptor.RouteValues["controller"];
            var result = (ViewResult)filterContext.Result;
            using (FileStream fs = new FileStream(_fullPath, FileMode.Append))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine($"The action {actionName} in {controllerName} controller " +
                        $"has the following ViewData {result.ViewData.Values.FirstOrDefault()}, event fired OnResultExecuted");
                }
            }
        }

    }
}
