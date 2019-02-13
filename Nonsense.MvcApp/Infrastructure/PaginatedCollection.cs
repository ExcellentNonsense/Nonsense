using Nonsense.Common.Utilities;
using System;
using System.Collections.Generic;

namespace Nonsense.MvcApp.Infrastructure {

    public class PaginatedCollection<T> {

        private int _totalPages { get; }
        public IEnumerable<T> Items { get; }
        public int CurrentPage { get; }

        public PaginatedCollection(IEnumerable<T> items, int currentPage, int itemsPerPage, int itemsCount) {
            Guard.NotNull(items, nameof(items));
            Guard.NotOutOfRange(itemsCount, nameof(itemsCount), 1, int.MaxValue);
            Guard.NotOutOfRange(itemsPerPage, nameof(itemsPerPage), 1, itemsCount);

            _totalPages = (int)Math.Ceiling(itemsCount / (double)itemsPerPage);

            Guard.NotOutOfRange(currentPage, nameof(currentPage), 1, _totalPages);

            Items = items;
            CurrentPage = currentPage;
        }

        public bool HasPreviousPage {
            get {
                return CurrentPage > 1;
            }
        }

        public bool HasNextPage {
            get {
                return CurrentPage < _totalPages;
            }
        }
    }
}
