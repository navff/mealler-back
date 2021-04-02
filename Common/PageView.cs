using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Common
{
    public class PageView<T>
    {
        public PageView()
        {
        }

        public List<T> Items { get; set; }
        public int PagesCount { get; set; }
        public int ItemsCount { get; set; }
        public int Page { get; set; }
        public bool HasPreviousPage => Page > 1;
        public bool HasNextPage => Page < PagesCount;

        public static async Task<PageView<T>> GetNewInstance(IQueryable<T> query, int page = 1)
        {
            var pageView = new PageView<T>();
            pageView.Page = page;
            pageView.ItemsCount = query.Count();
            pageView.PagesCount = (int) Math.Ceiling(query.Count() * 1.0 / HARDCODED_SETTINGS.ITEMS_PER_PAGE);
            pageView.Items = await query
                .Skip(HARDCODED_SETTINGS.ITEMS_PER_PAGE * (pageView.Page - 1))
                .Take(HARDCODED_SETTINGS.ITEMS_PER_PAGE)
                .ToListAsync();
            return pageView;
        }
    }
}