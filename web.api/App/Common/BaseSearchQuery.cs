using System;
using Common;
using MediatR;

namespace web.api.App.Common
{
    public abstract class BaseSearchQuery<T> : IRequest<PageView<T>>
    {
        private int _page = 1;

        public int Page
        {
            get => _page;
            set
            {
                if (value < 1) throw new ArgumentException("Page must be larger than '1'");
                _page = value;
            }
        }
    }
}