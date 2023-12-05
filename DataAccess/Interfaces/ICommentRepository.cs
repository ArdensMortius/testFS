using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<MyComment>> GetAllCommentsByDot(MyDot dot);
        Task<MyComment> GetCommentById(int id);        
        Task AddComment(MyComment comment);
        Task UpdateComment(MyComment comment);
        Task DeleteComment(int id);
    }
}
