namespace Tech.Interview.Application.Persistence.UoW
{
    public interface IUnitOfWorkRepository
    {
        IUserRepostory UserRepository { get; }
    }
}
