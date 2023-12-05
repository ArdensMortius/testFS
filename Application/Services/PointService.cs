using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Application.Services
{
    public class PointService : IPointService
    {
        private readonly IPointRepository _pointRepository;
        public PointService(IPointRepository pointRepository)
        {
            _pointRepository = pointRepository;
        }
        public async Task AddPointWithCoordinates(int x, int y)
        {            
            await _pointRepository.AddPoint(new MyDot(x, y));
        }

        public async Task DeletePoint(int id)
        {
            await _pointRepository.DeletePoint(id);            
        }

        public async Task<IEnumerable<MyDot>> GetAllPoints()
        {
            return await _pointRepository.GetAllPoints();            
        }

        public async Task<MyDot> GetPointById(int id)
        {
            return await _pointRepository.GetPointById(id);            
        }

        public async Task MovePoint(int id, int newX, int newY)
        {
            var d = await _pointRepository.GetPointById(id);
            if (d is null) throw new Exception("Point not found");
            d.Cord_x = newX;
            d.Cord_y = newY;
            await _pointRepository.UpdatePoint(d);
        }

        public async Task UpdatePointSize(int id, int radius)
        {
            var d = await _pointRepository.GetPointById(id);
            if (d is null) throw new Exception("Point not found");
            d.Radius = radius;            
            await _pointRepository.UpdatePoint(d);
        }

        public async Task UpdatePointColor(int id, int ARGBcolor)
        {
            var d = await _pointRepository.GetPointById(id);
            if (d is null) throw new Exception("Point not found");
            d.ARGBColor = ARGBcolor;
            await _pointRepository.UpdatePoint(d);
        }

        //скорее всего не будет работать
        public async Task AddCommentToSpecificDot(int DotId)
        {
            var d = await _pointRepository.GetPointById(DotId);
            if (d is null) throw new Exception("Point not found");
            //d.Comments.Add(new MyComment(d));
            await _pointRepository.UpdatePoint(d);            
        }
    }
}
