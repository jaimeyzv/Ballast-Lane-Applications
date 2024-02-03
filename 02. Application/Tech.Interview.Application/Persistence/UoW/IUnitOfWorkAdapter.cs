namespace Tech.Interview.Application.Persistence.UoW
{
    public interface IUnitOfWorkAdapter : IDisposable
    {
        IUnitOfWorkRepository Repositories { get; }
        Task SaveChangesAcync();
    }
}
