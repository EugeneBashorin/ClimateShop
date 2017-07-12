using ClimateStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ClimateStore.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");   //Construct an <a> tag
                tag.MergeAttribute("href", pageUrl(i)); //add new attributes to the tag
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                    tag.AddCssClass("selected");
                result.Append(tag.ToString());
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}
/*
 Метод расширения PageLinks генерирует HTML для набора ссылок на страницы, используя
информацию, предоставленную в объекте PagingInfo. Параметр Func предоставляет возможность
передачи делегата, который будет использоваться для генерации ссылок на другие страницы.
     */
