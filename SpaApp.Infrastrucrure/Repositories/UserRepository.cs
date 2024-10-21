using MongoDB.Bson;
using MongoDB.Driver;
using SpaApp.Domain.Entities;
using SpaApp.Domain.Repositories;
using SpaApp.Application.Common;

namespace SpaApp.Infrastrucrure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IMongoDatabase database) => _users = database.GetCollection<User>(Constants.CollectionNames.USERS);

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _users.Find(_ => true).ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _users.Find(u => u.Id == id).SingleOrDefaultAsync();
        }

        // Получить пользователя по имени
        public async Task<User> GetUserByNameAsync(string username)
        {
            return await _users.Find(u => u.Username == username).SingleOrDefaultAsync();
        }

        // Добавить нового пользователя
        public async Task AddUserAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }

        // Обновить пользователя
        public async Task UpdateUserAsync(User user)
        {
            await _users.ReplaceOneAsync(u => u.Id == user.Id, user);
        }

        // Удалить пользователя
        public async Task DeleteUserAsync(string id)
        {
            await _users.DeleteOneAsync(u => u.Id == id);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _users.Find(u => string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase)).AnyAsync();
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _users.Find(u => string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase)).AnyAsync();
        }
    }
}
