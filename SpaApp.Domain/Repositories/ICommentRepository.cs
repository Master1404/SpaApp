using SpaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaApp.Domain.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllCommentAsync();
        public Task<Comment> GetCommentByIdAsync(string id);
        //Task<User> GetUserByNameAsync(string username);
        Task AddCommentAsync(Comment сomment);
        Task UpdateCommentAsync(Comment сomment);
        Task DeleteCommentAsync(string id);
    }
}
