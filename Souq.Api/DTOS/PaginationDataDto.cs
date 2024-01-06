using Souq.Core.DataBase;

namespace Souq.Api.DTOS
{
    public class PaginationDataDto<T> 
    {


        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Count { get; set; }

        public IReadOnlyList<T> Data { get; set; }


        public PaginationDataDto(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }
        public PaginationDataDto()
        {
            
        }


    }
}
