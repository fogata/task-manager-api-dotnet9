namespace TaskManager.Core.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}
