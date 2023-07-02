using Microsoft.EntityFrameworkCore;
using WebAppProject.DAL;
using WebAppProject.Models;

namespace Repository
{
    public class MyRepository : IMyRepository
    {
        readonly DataContext _context;

        public MyRepository(DataContext context)
        {
            _context = context;
            Animals = _context.Animals!;
            Categories = _context.Categories!;
            Comments = _context.Comments!;
        }

        // Tables
        public IEnumerable<Animal> Animals { get; }
        public IEnumerable<Category> Categories { get;}
        public IEnumerable<Comment> Comments { get; }

        //Aminals
        public IEnumerable<Animal> GetAnimalsWithComments() => _context.Animals!.Include(a => a.Comments);
       
        public void InsertAnimal(Animal animal)
        {
            _context.Animals!.Add(animal);
            _context.SaveChanges();
        }
        public void DeleteAnimal(Animal animal)
        {
            _context.Remove(animal);
            _context.SaveChanges();
        }
        public void EditAnimal(Animal newAnimal)
        {
            var animal = Animals.FirstOrDefault(a => a.Id.Equals(newAnimal.Id));
            if (animal != null)
            {
                animal.Name = newAnimal.Name;
                animal.Age = newAnimal.Age;
                animal.PictureName = newAnimal.PictureName;
                animal.Description = newAnimal.Description;
                animal.CategoryId = newAnimal.CategoryId;
                _context.SaveChanges();
            }
        }


        // Category
        public IEnumerable<Category> GetCategoriesWithAminals() => _context.Categories!.Include(c => c.Animals);


        // Comment
        public void AddComment(Comment comment)
        {
            _context.Comments!.Add(comment);
            _context.SaveChanges();
        }

        public void RemoveComment(Comment comment) 
        { 
            _context.Comments!.Remove(comment);
            _context.SaveChanges();
        }

        public void RemoveComments(IEnumerable<Comment> comments)
        {
            _context.Comments!.RemoveRange(comments);
            _context.SaveChanges();
        }

    }
}