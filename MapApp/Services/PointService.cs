using MapApp.Models;
using MapApp.Repositories;
using MapApp.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapApp.Services
{
    public class PointService : IPointService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PointService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<List<Point>>> GetAllAsync()
        {
            try
            {
                var points = await _unitOfWork.Points.GetAllAsync();
                return new Response<List<Point>>
                {
                    Success = true,
                    Data = points.OrderBy(p => p.Id).ToList()
                };
            }
            catch (Exception ex)
            {
                return new Response<List<Point>> { Success = false, Message = ex.Message };
            }
        }

        public async Task<Response<Point>> GetByIdAsync(int id)
        {
            try
            {
                var point = await _unitOfWork.Points.GetByIdAsync(id);
                if (point == null)
                {
                    return new Response<Point> { Success = false, Message = "Point not found" };
                }

                return new Response<Point> { Success = true, Data = point };
            }
            catch (Exception ex)
            {
                return new Response<Point> { Success = false, Message = ex.Message };
            }
        }

        public async Task<Response<Point>> AddAsync(Point point)
        {
            try
            {
                // Automatic ID assignment
                if (point.Id == 0)
                {
                    var existingPoints = await _unitOfWork.Points.GetAllAsync();
                    var existingIds = existingPoints.Select(p => p.Id).OrderBy(id => id).ToList();
                    int newId = 1;

                    foreach (var id in existingIds)
                    {
                        if (id == newId)
                        {
                            newId++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    point.Id = newId;
                }

                await _unitOfWork.Points.AddAsync(point);
                return new Response<Point> { Success = true, Data = point };
            }
            catch (Exception ex)
            {
                return new Response<Point> { Success = false, Message = ex.Message };
            }
        }

        public async Task<Response<Point>> UpdateAsync(Point point)
        {
            try
            {
                await _unitOfWork.Points.UpdateAsync(point);
                return new Response<Point> { Success = true, Data = point };
            }
            catch (Exception ex)
            {
                return new Response<Point> { Success = false, Message = ex.Message };
            }
        }

        public async Task<Response<Point>> DeleteAsync(int id)
        {
            try
            {
                await _unitOfWork.Points.DeleteAsync(id);
                return new Response<Point> { Success = true, Message = "Point deleted successfully" };
            }
            catch (Exception ex)
            {
                return new Response<Point> { Success = false, Message = ex.Message };
            }
        }
    }
}
