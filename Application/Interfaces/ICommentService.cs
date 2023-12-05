using Domain.Entities;
using System.Drawing;

namespace Application.Interfaces
{
    public interface ICommentService
    {
        //получить все коменты точки
        Task<IEnumerable<MyComment>> GetAllCommentsByDot(MyDot dot);
        //найти комент по Id
        Task<MyComment> GetCommentById(int id);        
        //обновить текст комента
        Task<MyComment> UpdateCommentText(int id, string new_text);
        //обновить цвет фона
        Task<MyComment> UpdateBackColor(int id, Color new_color);
        //удалить по ID
        Task DeleteComment(int id);
    }
}
