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
            var pages = _ctx.Pages.Where(a => a.AuthorId == id).Include(p => p.Author).ToListAsync();
            return await pages as List<Page>;
        }
        public async Task<List<Page>> GetPagesByTagsAsync(string tags)
        {

            // Split the input string of tags into an array
            string[] tagArray = tags.Split(',');

            // Query pages where any tag matches any of the specified tags
            var pages = await GetPagesAsync();

            List<Page> listPages = new List<Page>();

            foreach (Page page in pages) {
                if (page.Tags != null && tagArray.All(page.Tags.Contains)) 
                {
                    listPages.Add(page);
                }
            }
            return listPages;

        }
        public async Task<List<Page>> SearchPagesAsync(string? authorname, string? tags)
        {
            IQueryable<Page> query = _ctx.Pages.Include(p => p.Author);
            var pages = await query.ToListAsync();

            if (tags != null && tags != string.Empty)
            {
                string[] tagArray = tags.Split(',');
                pages = pages.Where(page => tagArray.Any(tag => page.Tags != null && page.Tags != string.Empty && page.Tags.Contains(tag))).ToList();
            }

            if (authorname != null && authorname != string.Empty)
            {
                pages = pages.Where(page => page.Author != null && page.Author.UserName == authorname).ToList();
            }

            return pages;
        }
        public async Task<Page> GetPageByIdAsync(int id)
        {
            return await _ctx.Pages
                             .Include(p => p.Author) // Eagerly load the Author property
                             .FirstOrDefaultAsync(p => p.Id == id); // Use FirstOrDefaultAsync to match by id
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
