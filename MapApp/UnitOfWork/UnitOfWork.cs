using MapApp.Data;
using MapApp.Repositories;
using System.Threading.Tasks;
using MapApp.Models;

namespace MapApp.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Points = new GenericRepository<Point>(_context);
        }

        public IGenericRepository<Point> Points { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
