using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPointService
    {
        //получить все точки
        Task<IEnumerable<MyDot>> GetAllPoints(); 
        //найти точку по ID
        Task<MyDot> GetPointById(int id);
        //обновить цвет
        Task UpdatePointColor(int id, int ARGBcolor);
        //обновить размер
        Task UpdatePointSize(int id, int radius);
        //удалить точку по ID
        Task DeletePoint(int id);
        //подвинуть точку
        Task MovePoint(int id, int newX, int newY);
        //создать точку по координатам
        Task AddPointWithCoordinates(int x, int y);
        //добавить комент к точке
        Task AddCommentToSpecificDot(int DotId);

    }
}
