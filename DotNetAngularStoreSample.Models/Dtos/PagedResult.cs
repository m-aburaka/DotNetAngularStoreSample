using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetAngularStoreSample.Models.Dtos
{
    public class PagedResult<T>
    {
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int ItemsCount { get; set; }
        public int PageNumber { get; set; }

        public IList<T> Result { get; set; } = new List<T>();
    }
}
