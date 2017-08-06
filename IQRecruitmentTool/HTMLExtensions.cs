using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Linq;
using System.Web;

namespace IQRecruitmentTool
{
    public static class HTMLExtensions
    {
       
            public static string FieldIdFor<T, TResult>
            (this HtmlHelper<T> html, Expression<Func<T, TResult>> expression)
            {
                var id = html.ViewData.TemplateInfo.GetFullHtmlFieldId
                (ExpressionHelper.GetExpressionText(expression));
                return id.Replace('[', '_').Replace(']', '_');
            }
        }
    }
