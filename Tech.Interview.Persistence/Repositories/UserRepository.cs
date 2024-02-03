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

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var command = CreateCommand("SELECT * FROM [dbo].[User] WITH(NOLOCK)");
            var users = new List<User>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var user = new User
                    {
                        Id = Convert.ToInt32(reader["UserId"]),
                        Name = reader["Name"].ToString(),
                        LastName = reader["Lastname"].ToString(),
                        Age = Convert.ToInt32(reader["Age"])
                    };

                    users.Add(user);
                }
            }

            return users;
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            var command = CreateCommand("SELECT * FROM [dbo].[User] WITH(NOLOCK) WHERE id = @userId");

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