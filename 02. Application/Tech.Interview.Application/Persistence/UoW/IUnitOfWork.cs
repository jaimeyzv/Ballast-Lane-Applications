namespace Tech.Interview.Application.Persistence.UoW
{
    public interface IUnitOfWork
    {
        IUnitOfWorkAdapter Create();
    }
}
