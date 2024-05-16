using Datalayer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataObjects;

namespace Datalayer.Repositories
{

    public class ReplyRepository : IReplyRepository
    {
        private readonly MyProjectContext _context;

        public ReplyRepository(MyProjectContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reply>> GetRepliesAsync()
        {
            return await _context.Replies.ToListAsync();
        }

        public async Task<Reply> GetReplyByIdAsync(int id)
        {
            return await _context.Replies.FindAsync(id);
        }

        public async Task<IEnumerable<Reply>> GetRepliesByAuthorAsync(int authorId)
        {
            return await _context.Replies.Where(r => r.AuthorId == authorId).ToListAsync();
        }

        public async Task<IEnumerable<Reply>> GetRepliesByPageAsync(int pageId)
        {
            return await _context.Replies.Where(r => r.PageId == pageId).Include(r => r.Author).ToListAsync();
        }

        public async Task CreateReplyAsync(Reply reply)
        {
            _context.Replies.Add(reply);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReplyAsync(Reply reply)
        {
            _context.Entry(reply).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReplyAsync(int id)
        {
            var reply = await _context.Replies.FindAsync(id);
            if (reply != null)
            {
                _context.Replies.Remove(reply);
                await _context.SaveChangesAsync();
            }
        }
    }
}
