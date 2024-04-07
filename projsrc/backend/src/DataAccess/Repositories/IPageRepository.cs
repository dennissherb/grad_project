using Datalayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Datalayer.Repositories
{
    public interface IPageRepository
    {
        Task<IEnumerable<Page>> GetPagesAsync();
        Task<Page> GetPagesByIdAsync(int id);
        Task CreatePageAsync(Page p);
        Task UpdatePageAsync(Page p);
        Task DeletePageAsync(Page p);
        Task DeletePageAsyncById(int id);
    }
}
