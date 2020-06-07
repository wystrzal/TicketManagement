using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagement.API.Dtos
{
    public class PaginatedItemsDto<TEntity> where TEntity : class
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Count { get; set; }

        public IEnumerable<TEntity> Data { get; set; }

        public PaginatedItemsDto(int pageIndex, int count, IEnumerable<TEntity> data, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.Count = count;
            this.Data = data;
        }
    }
}
