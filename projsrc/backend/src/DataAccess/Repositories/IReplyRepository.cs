﻿using DataObjects;

namespace Datalayer.Repositories
{
    public interface IReplyRepository
    {
        Task<IEnumerable<Reply>> GetRepliesAsync();
        Task<Reply> GetReplyByIdAsync(int id);
        Task<IEnumerable<Reply>> GetRepliesByAuthorAsync(int authorId);
        Task<IEnumerable<Reply>> GetRepliesByPageAsync(int pageId);
        Task CreateReplyAsync(Reply reply);
        Task UpdateReplyAsync(Reply reply);
        Task DeleteReplyAsync(int id);
    }
}
