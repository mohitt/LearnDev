using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.Web.Mvc;

namespace Ads.Helper
{
    public static class CustomUrlHelper
    {
        public static MvcHtmlString To<Tcontroller>(this UrlHelper helper, Expression<Action<Tcontroller>> action) where Tcontroller : Controller {
            // based on Microsoft.Web.Mvc.dll LinkBuilder
            return MvcHtmlString.Create(LinkBuilder.BuildUrlFromExpression<Tcontroller>(helper.RequestContext, helper.RouteCollection, action));
        }

    }
}