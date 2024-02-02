using System.Data.SqlClient;
using Tech.Interview.Application.Persistence;
using Tech.Interview.Domain.Entities;

namespace Tech.Interview.Persistence.Repositories
{
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(SqlConnection context, SqlTransaction transaction)
        {
            this._context = context;
            this._transaction = transaction;
        }

        public Task<Guid> CreateUserAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            var command = CreateCommand("SELECT * FROM Users WITH(NOLOCK) WHERE id = @userId");

            command.Parameters.AddWithValue("@userId", userId);

            using (var reader = command.ExecuteReader())
            {
                reader.Read();

                return new User
                {
                    Name = reader["name"].ToString(),
                    LastName = reader["id"].ToString(),
                    Age = Convert.ToInt32(reader["price"])                    
                };
            }
        }

        public Task<bool> UpdateUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}