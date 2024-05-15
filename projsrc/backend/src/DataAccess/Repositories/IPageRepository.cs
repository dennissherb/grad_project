using Datalayer.Models;
using DataObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Datalayer.Repositories
{
    public interface IPageRepository
    {
        Task<IEnumerable<Page>> GetPagesAsync();
        Task<Page> GetPageByIdAsync(int id);
        Task<List<Page>> GetPagesByAuthorAsync(int id);
        Task<List<Page>> GetPagesByTagsAsync(string tags);
        Task CreatePageAsync(Page p);
        Task UpdatePageAsync(Page p);
        Task DeletePageAsync(int id);
        Task<List<Page>> SearchPagesAsync(string? authorname, string tags);
    }
}
