using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace SSU.Reception.Helpers
{
    public static class HtmlHelperExtension
    {
        public static MvcHtmlString BoolToString(this HtmlHelper helper, bool value)
        {
            return MvcHtmlString.Create(value ? "ДА" : "НЕТ");
        }

        public static MvcHtmlString PagingPrev<Tmodel>(this HtmlHelper<Tmodel> helper, int pageNum)
        {
            StringBuilder sb = new StringBuilder();

            if (pageNum > 0)
            {
                sb.Append(helper.ActionLink("< Назад", "Index", new { pageNum = pageNum - 1 }, new { @id="paging_prev", @class = "btn btn-secondary" }));
            }

            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString PagingNext<Tmodel>(this HtmlHelper<Tmodel> helper, int pageNum, int itemsCount, int pageSize)
        {
            StringBuilder sb = new StringBuilder();

            int pagesCount = (int)Math.Ceiling((double)itemsCount / pageSize);

            if (pageNum < pagesCount - 1)
            {
                sb.Append(helper.ActionLink("Вперёд >", "Index", new { pageNum = pageNum + 1 }, new { @id = "paging_next", @class = "btn btn-secondary" }));
            }

            return MvcHtmlString.Create(sb.ToString());
        }
    }
}