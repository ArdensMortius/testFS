using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPointRepository
    {
        Task<IEnumerable<MyDot>> GetAllPoints();
        Task<MyDot> GetPointById(int id);
        Task AddPoint(MyDot point);
        Task UpdatePoint(MyDot point);
        Task DeletePoint(int id);
    }
}
