using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

namespace Infrastructure.Services
{
    public class PointRepository : IPointRepository
    {
        private readonly MyDotsDBContext _dbContext;
        public PointRepository(MyDotsDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddPoint(MyDot point)
        {
            var result = await _dbContext.Dots.AddAsync(point);
            await _dbContext.SaveChangesAsync();            
        }

        public async Task DeletePoint(int id)
        {
            var existingPoint = await _dbContext.Dots.FirstOrDefaultAsync(p => p.ID == id);
            if (existingPoint == null)
            {
                return;
            }
            _dbContext.Dots.Remove(existingPoint);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<MyDot>> GetAllPoints()
        {
            return await _dbContext.Dots.Include(p => p.Comments).ToListAsync();
        }

        public async Task<MyDot> GetPointById(int id)
        {
            return await _dbContext.Dots.Include(p => p.Comments).FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task UpdatePoint(MyDot point)
        {
            var existingPoint = await _dbContext.Dots.FirstOrDefaultAsync(p => p.ID == point.ID);
            if (existingPoint == null) return;            
            existingPoint.Cord_x = point.Cord_x;
            existingPoint.Cord_y = point.Cord_y;
            existingPoint.Radius = point.Radius;
            existingPoint.ARGBColor = point.ARGBColor; 
            existingPoint.Comments = point.Comments;
            await _dbContext.SaveChangesAsync();
        }
    }
}
