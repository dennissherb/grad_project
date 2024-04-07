using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datalayer.Models;
using Datalayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Datalayer.Repositories
{
    public class PageRepository : IPageRepository
    {
        private readonly PageContext _ctx;
        public PageRepository(PageContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<Page>> GetPagesAsync()
        {
            var pages = await _ctx.Pages.ToListAsync();
            return (IEnumerable<Page>)pages;
        }
        public async Task<Page> GetPagesByIdAsync(int id)
        {
            return await _ctx.Pages.FindAsync(id);
        }

        public async Task CreatePageAsync(Page p)
        {
            var createPage = _ctx.Pages.Add(p);
            await _ctx.SaveChangesAsync();
        }
        public async Task UpdatePageAsync(Page p)
        {
            var updatePage = _ctx.Pages.Update(p);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeletePageAsync(Page p)
        {
            var page = await _ctx.Pages.FindAsync(p.Id);
            if (page != null)
            {
                _ctx.Pages.Remove(page);
                await _ctx.SaveChangesAsync();
            }
        }
        public async Task DeletePageByIdAsync(int id)
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
