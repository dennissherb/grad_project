using Datalayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Datalayer.Repositories
{
    public interface IPageRepository
    {
        Task<IEnumerable<Page>> GetPagesAsync();
        Task<Page> GetPageByIdAsync(int id);
        Task CreatePageAsync(Page p);
        Task UpdatePageAsync(Page p);
        Task DeletePageAsync(int id);
    }
}
