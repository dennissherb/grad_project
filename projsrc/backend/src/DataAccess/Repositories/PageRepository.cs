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
    public class PageRepository
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

    }
}
