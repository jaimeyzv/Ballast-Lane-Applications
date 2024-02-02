using Microsoft.Extensions.Configuration;
using Tech.Interview.Application.Persistence.UoW;

namespace Tech.Interview.Persistence.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConfiguration _configuration;

        public UnitOfWork(IConfiguration configuration = null)
        {
            _configuration = configuration;
        }

        public IUnitOfWorkAdapter Create()
        {
            var connectionString = _configuration.GetConnectionString("InterviewDB");
            return new UnitOfWorkAdapter(connectionString);
        }
    }
}
