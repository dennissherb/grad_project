using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datalayer.Models;
using Datalayer.Repositories;
using Microsoft.EntityFrameworkCore;
using DataObjects;

namespace Datalayer.Repositories
{
    public class PageRepository : IPageRepository
    {
        private readonly MyProjectContext _ctx;
        public PageRepository(MyProjectContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<Page>> GetPagesAsync()
        {
            var pages = await _ctx.Pages.ToListAsync();
            return (IEnumerable<Page>)pages;
        }
        public async Task<List<Page>> GetPagesByAuthorAsync(int id)
        {
            var pages = _ctx.Pages.Where(a => a.AuthorId == id).ToListAsync();
            return await pages as List<Page>;
        }
        public async Task<Page> GetPageByIdAsync(int id)
        {
            return await _ctx.Pages.FindAsync(id);
        }

        public async Task CreatePageAsync(Page p)
        {
            _ctx.Pages.Add(p);
            await _ctx.SaveChangesAsync();
        }
        public async Task UpdatePageAsync(Page p)
        {
            _ctx.Pages.Update(p);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeletePageAsync(int id)
        {
            var page = await _ctx.Pages.FindAsync(id);
            if (page != null)
            {
                _ctx.Pages.Remove(page);
                await _ctx.SaveChangesAsync();
            }
        }
    }
}
