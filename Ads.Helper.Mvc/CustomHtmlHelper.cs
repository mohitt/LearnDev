using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using Microsoft.Web.Mvc;

namespace Ads.Helper
{
    public static class CustomHtmlHelper
    {
        public static MvcHtmlString AllController(this HtmlHelper helper, Assembly assembley) {
            var sortedLinks = new SortedList<string, string>();

            assembley.GetTypes()
                     .Where(k => k.Name.EndsWith("Controller") && k.GetMethods().Any(methodInf => methodInf.Name == "Index"))
                     .ToList()
                     .ForEach(ctrlr => {
                         var controllerName = ctrlr.Name.Substring(0, ctrlr.Name.IndexOf("Controller"));
                         var orderAttr =
                             ctrlr.GetCustomAttributes(typeof(LinkOrderAttribute), false).FirstOrDefault()
                             as LinkOrderAttribute;
                         sortedLinks.Add(orderAttr == null ? controllerName : orderAttr.Key, String.Format(@"<li><a href=""/{0}/"">{0}</a></li>" + Environment.NewLine, controllerName));
                     });

            return MvcHtmlString.Create(sortedLinks.Aggregate("", (total, current) => total += current.Value));

        }
        public static MvcHtmlString Button<Tcontroller>(this HtmlHelper helper, string name, string text, Expression<Action<Tcontroller>> action, object htmlAttributes) where Tcontroller : Controller {
            var dic = htmlAttributes.GetType()
             .GetProperties()
             .ToDictionary(p => p.Name, p => p.GetValue(htmlAttributes, null));

            dic.Add("custom-action", helper.To<Tcontroller>(action));
            return helper.Button(name, text, HtmlButtonType.Submit,
                          "$(this).parents('form:first').attr('action',$(this).attr('custom-action'));",
                          dic);
        }


        public static string To<Tcontroller>(this HtmlHelper helper, Expression<Action<Tcontroller>> action) where Tcontroller : Controller {
            // based on Microsoft.Web.Mvc.dll LinkBuilder
            return LinkBuilder.BuildUrlFromExpression<Tcontroller>(helper.ViewContext.RequestContext, helper.RouteCollection, action);
        }

        public static MvcHtmlString GoogleAnalytics(this HtmlHelper helper, string accountId) {
#if !DEBUG

            return
                MvcHtmlString.Create(@"<script type=""text/javascript"">

                        var _gaq = _gaq || [];
                        _gaq.push(['_setAccount', '{0}']);
                        _gaq.push(['_trackPageview']);

                        (function () {
                            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
                            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
                            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
                        })();

                        </script>"
                    .Replace("{0}", accountId)
                    );
#else
            return MvcHtmlString.Create("");
#endif

        }

        public static string GetPropertyName<TObject>(this TObject type,
                                                   Expression<Func<TObject, object>> propertyRefExpr) {
            return GetPropertyNameCore(propertyRefExpr.Body);
        }

        public static string GetName<TObject>(Expression<Func<TObject, object>> propertyRefExpr) {
            return GetPropertyNameCore(propertyRefExpr.Body);
        }


        private static string GetPropertyNameCore(Expression propertyRefExpr) {
            if (propertyRefExpr == null)
                throw new ArgumentNullException("propertyRefExpr", "propertyRefExpr is null.");

            MemberExpression memberExpr = propertyRefExpr as MemberExpression;
            if (memberExpr == null) {
                UnaryExpression unaryExpr = propertyRefExpr as UnaryExpression;
                if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
                    memberExpr = unaryExpr.Operand as MemberExpression;
            }

            if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
                return memberExpr.Member.Name;

            throw new ArgumentException("No property reference expression was found.",
                             "propertyRefExpr");
        }

        public static string FormatWith(this string format, params object[] args) {
            if (format == null)
                throw new ArgumentNullException("format");
            return string.Format(format, args);
        }

        public static string FormatWith(this string format, IFormatProvider provider, params object[] args) {
            if (format == null)
                throw new ArgumentNullException("format");
            return string.Format(provider, format, args);
        }
    }
}
