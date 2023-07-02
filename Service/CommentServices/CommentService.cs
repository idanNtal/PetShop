using Microsoft.CodeAnalysis.CSharp.Syntax;
using Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppProject.Models;

namespace Service.CommentServices
{
    public class CommentService : ICommentService
    {
        private IMyRepository _repo;

        public CommentService(IMyRepository repository) => _repo = repository;


        private IEnumerable<Comment> GetAllComments() => _repo.Comments.ToList();
        public Task<IEnumerable<Comment>> GetAllCommentsAsync() => Task.Run(() => GetAllComments());


        private Comment GetCommentsByID(int commentID) => _repo.Comments.FirstOrDefault(c => c.Id == commentID);
        public Task<Comment> GetCommentsByIDAsync(int commentID) => Task.Run(() => GetCommentsByID(commentID));


        private IEnumerable<Comment> GetAllCommentsByUser(string UserId) => _repo.Comments.Where(c => c.UserId == UserId).ToList();
        public Task<IEnumerable<Comment>> GetAllCommentsByUserAsync(string UserId) => Task.Run(() => GetAllCommentsByUser(UserId));


        private IEnumerable<Comment> GetCommentsByAnimalId(int animalId) => _repo.Animals.First(c => c.Id == animalId).Comments!.ToList();
        public Task<IEnumerable<Comment>> GetCommentsByAnimalIdAsync(int animalID) => Task.Run(() => GetCommentsByAnimalId(animalID));


        private void AddComment(int animalId, string comment) => _repo.AddComment(new Comment { AnimalId = animalId, comment = comment });
        public Task AddCommentAsync(int animalId, string comment) => Task.Run(() => AddComment(animalId, comment));


        private void AddComment(Comment comment) => _repo.AddComment(comment);
        public Task AddCommentAsync(Comment comment) => Task.Run(() => AddComment(comment));


        private void RemoveComment(Comment comment) => _repo.RemoveComment(comment);
        public Task RemoveCommentAsync(Comment comment) => Task.Run(() => RemoveComment(comment));


        private void RemoveComments(IEnumerable<Comment> comments) => _repo.RemoveComments(comments);
        public Task RemoveCommentsAsync(IEnumerable<Comment> comments) => Task.Run(() => RemoveComments(comments));


        private IEnumerable<Comment> GetCommentsByIds(IEnumerable<int> IDs) => _repo.Comments!.Where(c => IDs.Contains(c.Id));
        public Task<IEnumerable<Comment>> GetCommentsByIdsAsync(IEnumerable<int> IDs) => Task.Run(() => GetCommentsByIds(IDs));
    }
}