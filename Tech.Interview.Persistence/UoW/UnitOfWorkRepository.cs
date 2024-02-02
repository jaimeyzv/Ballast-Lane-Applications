using System.Data.SqlClient;
using Tech.Interview.Application.Persistence;
using Tech.Interview.Application.Persistence.UoW;
using Tech.Interview.Persistence.Repositories;

namespace Tech.Interview.Persistence.UoW
{
    internal class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        public IUserRepository UserRepository { get; }

        public UnitOfWorkRepository(SqlConnection context, SqlTransaction transaction)
        {
            UserRepository = new UserRepository(context, transaction);
        }
    }
}
