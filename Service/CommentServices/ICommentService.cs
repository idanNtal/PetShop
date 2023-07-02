using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppProject.Models;

namespace Service.CommentServices
{
    public interface ICommentService
    {
        Task<Comment> GetCommentsByIDAsync(int commentID);
        Task<IEnumerable<Comment>> GetAllCommentsAsync();
        Task<IEnumerable<Comment>> GetAllCommentsByUserAsync(string UserId);
        Task<IEnumerable<Comment>> GetCommentsByAnimalIdAsync(int animalID);
        Task AddCommentAsync(int animalId, string comment);
        Task AddCommentAsync(Comment comment);
        Task RemoveCommentAsync(Comment comment);
        Task RemoveCommentsAsync(IEnumerable<Comment> comments);
        Task<IEnumerable<Comment>> GetCommentsByIdsAsync(IEnumerable<int> IDs);
    }
}
