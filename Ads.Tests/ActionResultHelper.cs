using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;

namespace Ads.Tests
{
   public static class ActionResultHelper
    {
        public static ViewResult ReturnsViewResult(this ActionResult result) {
            ViewResult castedresult = result as ViewResult;
            Assert.NotNull(castedresult);
            return castedresult;
        }

        public static PartialViewResult ReturnsPartialViewResult(this ActionResult result) {
            PartialViewResult castedresult = result as PartialViewResult;
            Assert.NotNull(castedresult);
            return castedresult;
        }

        public static ContentResult ReturnsContentResult(this ActionResult result) {
            var contentResult = result as ContentResult;
            Assert.NotNull(contentResult);
            return contentResult;
        }

        public static RedirectToRouteResult ReturnsRedirectToRouteResult(this ActionResult result) {
            var redirectResult = result as RedirectToRouteResult;
            Assert.NotNull(redirectResult);
            return redirectResult;
        }

        public static RedirectToRouteResult RedirectsTo<T>(this RedirectToRouteResult result, Expression<Action<T>> action)
        {
            var body = action.Body as MethodCallExpression;
            if (body == null) {
                throw new ArgumentException("Expression must be a method call.");
            }

            if (body.Object != action.Parameters[0]) {
                throw new ArgumentException("Method call must target lambda argument.");
            }

            string actionName = body.Method.Name;

            var attributes = body.Method.GetCustomAttributes(typeof(ActionNameAttribute), false);
            if (attributes.Length > 0) {
                var actionNameAttr = (ActionNameAttribute)attributes[0];
                actionName = actionNameAttr.Name;
            }

            string controllerName = typeof(T).Name;

            if (controllerName.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)) {
                controllerName = controllerName.Remove(controllerName.Length - 10, 10);
            }
            //values.Add("controller", controllerName);
            //values.Add("action", actionName);
            Assert.AreEqual(result.RouteValues["action"], actionName);
            Assert.AreEqual(result.RouteValues["controller"], controllerName);
            return result;
        }


        //protected RedirectToRouteResult RedirectToAction<T>(Expression<Action<T>> action, RouteValueDictionary values) where T : Controller {
        //    var body = action.Body as MethodCallExpression;

        //    if (body == null) {
        //        throw new ArgumentException("Expression must be a method call.");
        //    }

        //    if (body.Object != action.Parameters[0]) {
        //        throw new ArgumentException("Method call must target lambda argument.");
        //    }

        //    string actionName = body.Method.Name;

        //    var attributes = body.Method.GetCustomAttributes(typeof(ActionNameAttribute), false);
        //    if (attributes.Length > 0) {
        //        var actionNameAttr = (ActionNameAttribute)attributes[0];
        //        actionName = actionNameAttr.Name;
        //    }

        //    string controllerName = typeof(T).Name;

        //    if (controllerName.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)) {
        //        controllerName = controllerName.Remove(controllerName.Length - 10, 10);
        //    }

        //    RouteValueDictionary defaults = LinkBuilder.BuildParameterValuesFromExpression(body) ?? new RouteValueDictionary();

        //    values = values ?? new RouteValueDictionary();
        //    values.Add("controller", controllerName);
        //    values.Add("action", actionName);

        //    if (defaults != null) {
        //        foreach (var pair in defaults.Where(p => p.Value != null)) {
        //            values.Add(pair.Key, pair.Value);
        //        }
        //    }

        //    return new RedirectToRouteResult(values);
        //}



        public static PartialViewResult ForView(this PartialViewResult result, string viewName) {
            Assert.AreEqual(viewName, result.ViewName);
            return result;
        }

        public static ViewResult ForView(this ViewResult result, string viewName) {
            Assert.AreEqual(viewName, result.ViewName);
            return result;
        }

        public static ViewResult ForMaster(this ViewResult result, string masterName) {
            Assert.AreEqual(masterName, result.MasterName);
            return result;
        }

        

        public static TViewModel WithViewModel<TViewModel>(this ViewResult result) where TViewModel : class {
            TViewModel viewData = result.ViewData.Model as TViewModel;
            Assert.NotNull(viewData);
            return viewData;
        }

        public static TType WithViewData<TType>(this ViewResult result, string key) where TType : class {
            TType viewData = result.ViewData[key] as TType;
            Assert.NotNull(viewData);
            return viewData;
        }
    }
}
