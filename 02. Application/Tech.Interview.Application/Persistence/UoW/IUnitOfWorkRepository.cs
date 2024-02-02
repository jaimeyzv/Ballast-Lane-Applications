namespace Tech.Interview.Application.Persistence.UoW
{
    public interface IUnitOfWorkRepository
    {
        IUserRepository UserRepository { get; }
    }
}
