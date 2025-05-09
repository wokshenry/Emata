using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emata.Shared.Shared
{
    public class PagedList<T> : List<T>
    {

        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }

        public PagedList(IQueryable<T> source, int currentPage, int pageSize)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = source.Count();
            TotalPages = GetPages(PageSize, TotalCount);

            AddRange(source.Skip(CurrentPage * PageSize).Take(PageSize));
        }

        public PagedList(List<T> source, int totalCount, int currentPage, int pageSize)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = GetPages(PageSize, TotalCount);

            AddRange(source);
        }

        public PagedList(List<T> source, int totalCount, IPagedRequest pagedRequest)
        {
            CurrentPage = pagedRequest.CurrentPage;
            PageSize = pagedRequest.PageSize;
            TotalCount = totalCount;
            TotalPages = GetPages(PageSize, TotalCount);

            AddRange(source);
        }



        private PagedList()
        {

        }

        public static PagedList<T> Empty() => new();

        public bool HasPreviousPage
        {
            get
            {
                return (CurrentPage > 0);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (CurrentPage + 1 < TotalPages);
            }
        }

        private int GetPages(int pageSize, int count) => pageSize > 0
            ? (int)Math.Ceiling(count / (double)pageSize)
            : 0;
    }
}
