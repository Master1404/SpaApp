using MongoDB.Bson;
using MongoDB.Driver;
using SpaApp.Domain.Entities;
using SpaApp.Domain.Repositories;
using SpaApp.Application.Common;

namespace SpaApp.Infrastrucrure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IMongoCollection<Comment> _comment;

        public CommentRepository(IMongoDatabase database) => _comment = database.GetCollection<Comment>(Constants.CollectionNames.COMMENTS);
        public async Task<IEnumerable<Comment>> GetAllCommentAsync()
        {
            return await _comment.Find(_ => true).ToListAsync();
        }

        public async Task<Comment> GetCommentByIdAsync(string id)
        {
            return await _comment.Find(comment => comment.Id == id).FirstOrDefaultAsync();
        }
        public async Task AddCommentAsync(Comment comment)
        {
            await _comment.InsertOneAsync(comment);
        }
        public async Task UpdateCommentAsync(Comment comment)
        {
            await _comment.ReplaceOneAsync(c => c.Id == comment.Id, comment);
        }
        public async Task DeleteCommentAsync(string id)
        {
            await _comment.DeleteOneAsync(c => c.Id == id);
        }
    }
}
