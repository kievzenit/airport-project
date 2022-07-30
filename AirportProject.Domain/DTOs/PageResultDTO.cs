using System.Collections.Generic;

namespace AirportProject.Domain.DTOs
{
    public class PageResultDTO<TItems>
        where TItems : DTO
    {
        public ICollection<TItems> Items { get; private set; }
        public int TotalCount { get; private set; }

        public PageResultDTO(ICollection<TItems> items, int totalCount)
        {
            this.Items = items;
            this.TotalCount = totalCount;
        }
    }
}
