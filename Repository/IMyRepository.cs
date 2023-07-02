using WebAppProject.Models;

namespace Repository
{
    public interface IMyRepository
    {
        // Tables
         IEnumerable<Animal> Animals { get; }
         IEnumerable<Category> Categories { get; }
         IEnumerable<Comment> Comments { get; }
        
        // Animals
        IEnumerable<Animal> GetAnimalsWithComments();
        void InsertAnimal(Animal animal);
        void DeleteAnimal(Animal animal);
        void EditAnimal( Animal animal);


        // Categories
        IEnumerable<Category> GetCategoriesWithAminals();


        // Comments
        public void AddComment(Comment comment);
        public void RemoveComment(Comment comment);
        public void RemoveComments(IEnumerable<Comment> comments);
    }
}