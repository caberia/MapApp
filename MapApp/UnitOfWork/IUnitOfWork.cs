using System.Threading.Tasks;
using MapApp.Repositories;
using MapApp.Models;

namespace MapApp.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<Point> Points { get; }
        Task<int> CompleteAsync();
    }
}
