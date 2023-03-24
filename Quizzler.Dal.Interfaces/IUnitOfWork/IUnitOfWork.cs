using Microsoft.EntityFrameworkCore;

namespace Quizzler.Dal.Interfaces.IUnitOfWork;

public interface IUnitOfWork : IDisposable
{
    DbContext Context { get; }

    void Commit();
}