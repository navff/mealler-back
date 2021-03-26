using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Common
{
    public class PageView<T>
    {
        private PageView()
        {
        }

        public List<T> Items { get; private set; }
        public int PagesCount { get; private set; }
        public int ItemsCount { get; private set; }
        public int Page { get; set; }
        public bool HasPreviousPage => Page > 1;
        public bool HasNextPage => Page < PagesCount;

        public static async Task<PageView<T>> GetNewInstance(IQueryable<T> query)
        {
            var pageView = new PageView<T>();

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