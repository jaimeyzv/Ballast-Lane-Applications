using System.Data.SqlClient;
using Tech.Interview.Application.Persistence.Repositories;
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

        public async Task CreateUserAsync(User entity)
        {
            var query = $"INSERT INTO  [dbo].[User](Name, Lastname, Age) " +
                $"VALUES (@Name, @Lastname, @Age)";
            var command = CreateCommand(query);
            command.Parameters.AddWithValue("@Name", entity.Name);
            command.Parameters.AddWithValue("@Lastname", entity.LastName);
            command.Parameters.AddWithValue("@Age", entity.Age);
            await command.ExecuteNonQueryAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var command = CreateCommand("SELECT * FROM [dbo].[User] WITH(NOLOCK)");
            var users = new List<User>();

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    var user = new User
                    {
                        UserId = Convert.ToInt32(reader["UserId"]),
                        Name = reader["Name"].ToString(),
                        LastName = reader["Lastname"].ToString(),
                        Age = Convert.ToInt32(reader["Age"])
                    };

                    users.Add(user);
                }
            }

            return users;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var command = CreateCommand("SELECT * FROM [dbo].[User] WITH(NOLOCK) WHERE UserId = @UserId");
            command.Parameters.AddWithValue("@UserId", userId);

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    return new User
                    {
                        UserId = Convert.ToInt32(reader["UserId"]),
                        Name = reader["Name"].ToString(),
                        LastName = reader["Lastname"].ToString(),
                        Age = Convert.ToInt32(reader["Age"])
                    };
                }
            }

            return null;
        }

        public async Task UpdateUserAsync(User entity)
        {
            var query = $"UPDATE [dbo].[User]" +
                $" SET Name = @Name, Lastname = @Lastname, Age = @Age" +
                $" WHERE UserId = @userId ";
            var command = CreateCommand(query);
            command.Parameters.AddWithValue("@Name", entity.Name);
            command.Parameters.AddWithValue("@Lastname", entity.LastName);
            command.Parameters.AddWithValue("@Age", entity.Age);
            command.Parameters.AddWithValue("@UserId", entity.UserId);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var query = $"DELETE FROM [dbo].[User]" +
                $" WHERE UserId = @userId ";
            var command = CreateCommand(query);
            command.Parameters.AddWithValue("@UserId", userId);

            await command.ExecuteNonQueryAsync();
        }
    }
}