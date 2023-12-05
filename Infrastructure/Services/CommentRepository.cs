using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CommentRepository : ICommentRepository
    {
        private readonly MyDotsDBContext _dbContext;
        public CommentRepository(MyDotsDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddComment(MyComment comment)
        {
            //нельзя добавить комент к несуществующей точке
            var d = await _dbContext.Dots.Where(d => d == comment.Dot).FirstOrDefaultAsync();
            //сообщений об ошибке нет, просто ничего не будет создано
            if (d == null) return;
            await _dbContext.AddAsync(comment);
            await _dbContext.SaveChangesAsync();            
        }

        public async Task DeleteComment(int id)
        {
            var existingComment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.ID == id);
            if (existingComment == null) return;
            _dbContext.Comments.Remove(existingComment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<MyComment>> GetAllCommentsByDot(MyDot dot)
        {
            return await _dbContext.Comments.Include(c => c.Dot).Where(c=>c.Dot == dot).ToListAsync();
        }

        public async Task<MyComment> GetCommentById(int id)
        {
            return await _dbContext.Comments.Include(c => c.Dot).FirstOrDefaultAsync(c => c.ID == id);
        }

        public async Task UpdateComment(MyComment comment)
        {
            var existingComment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.ID == comment.ID);
            if (existingComment == null) return;
            existingComment.Text = comment.Text;
            //коомент не может переехать к другой точке
            //existingComment.Dot = comment.Dot; 
            existingComment.intcolor = comment.intcolor;
            await _dbContext.SaveChangesAsync();            
        }
    }
}
