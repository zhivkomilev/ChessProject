using Chess.Core.DataAccess.Repositories;
using System;
using System.Threading.Tasks;

namespace Chess.Core.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<TRepositoryInterface> RepositoryAsync<TRepositoryInterface>()
            where TRepositoryInterface : IRepository;

        Task RollbackAsync();

        Task<int> CommitAsync();
    }
}
