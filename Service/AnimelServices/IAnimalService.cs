using WebAppProject.Models;

namespace Service.AnimelsServices
{
    public interface IAnimalService
    {
        Task<IEnumerable<Animal>> GetAnimalsAsync();
        Task<Animal> GetAnimalByIdAsync(int id);
        Task<IEnumerable<Animal>> GetTopCommentsAminalsAsync(int HowMany);
        Task<IEnumerable<Animal>> GetAnimalsByCategoryAsync(int categoryId);
        Task InsertAnimalAsync(Animal animal);
        Task DeleteAnimalAsync(int id);
        Task EditAnimalAsync(Animal animal);
        
        bool IsNameAvailable(string name, int animalId);
        bool IsIdExist(int id);
    }
}
