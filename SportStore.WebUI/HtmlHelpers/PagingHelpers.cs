using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SportStore.WebUI.Models;

namespace SportStore.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        //метод расширения PageLinks генерирует ХТМЛ для набора ссылок на страницы используя информацию представленную в обьекте PagingInfo.
        //параметр Func предоставляет возможность передачи делегата который будет использоваться для генерации ссылок на другие страницы.
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a"); // Construct an <a> tag
                tag.MergeAttribute("href",pageUrl(i));
                tag.InnerHtml = i.ToString();
                    //если текущая страница, то выделяем ее, например добавляя класс
                if(i == pagingInfo.CurrentPage)
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                result.Append(tag.ToString());
            }

            return MvcHtmlString.Create(result.ToString());
        }
        //данный хелпер создает блок ссылок а также добавляет им классы для визуализации. Классы могут быть любыми
    }
}