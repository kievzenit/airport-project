using System.Collections.Generic;

namespace AirportProject.Domain.DTOs
{
    public class PageResultDTO<TItems>
        where TItems : DTO
    {
        public ICollection<TItems> Items { get; private set; }
        public int TotalCount { get => this.Items.Count; }

        public PageResultDTO(ICollection<TItems> items)
        {
            this.Items = items;
        }
    }
}
