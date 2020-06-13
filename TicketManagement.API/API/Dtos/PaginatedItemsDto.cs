using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagement.API.Dtos
{
    public class PaginatedItemsDto<TEntity, TCount> where TEntity : class where TCount : class
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public IEnumerable<TEntity> Data { get; set; }

        public TCount Count { get; set; }

        public PaginatedItemsDto(int pageIndex, TCount count, IEnumerable<TEntity> data, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.Count = count;
            this.Data = data;
        }
    }
}
