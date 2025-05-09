using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emata.Shared.Shared
{
    public interface IPagedRequest
    {
        public int CurrentPage { get; }

        public int PageSize { get; }
    }
}
