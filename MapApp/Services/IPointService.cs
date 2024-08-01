namespace MapApp.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MapApp.Models;

    public interface IPointService
    {
        Task<Response<List<Point>>> GetAllAsync();
        Task<Response<Point>> GetByIdAsync(int id);
        Task<Response<Point>> AddAsync(Point point);
        Task<Response<Point>> UpdateAsync(Point point);
        Task<Response<Point>> DeleteAsync(int id);
    }
}
