using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public Task AddCommentToSpecificDot(int DotId)
        {
            _commentRepository.AddComment(new MyComment());
            //throw new NotImplementedException();
        }
        public async Task DeleteComment(int id)
        {
            await _commentRepository.DeleteComment(id);
        }
        //+
        public async Task<IEnumerable<MyComment>> GetAllCommentsByDot(MyDot dot)
        {
            return await _commentRepository.GetAllCommentsByDot(dot);            
        }
        //+
        public async Task<MyComment> GetCommentById(int id)
        {
            return await _commentRepository.GetCommentById(id);
        }

        public Task<MyComment> UpdateBackColor(int id, Color new_color)
        {
            throw new NotImplementedException();
        }

        public Task<MyComment> UpdateCommentText(int id, string new_text)
        {
            throw new NotImplementedException();
        }
    }
}
