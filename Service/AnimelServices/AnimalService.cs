using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Repository;
using WebAppProject.Models;

namespace Service.AnimelsServices
{
    public class AnimalService : IAnimalService
    {
        private readonly IMyRepository _repo;

        public AnimalService(IMyRepository repository) => _repo = repository;


        // Animals
        private IEnumerable<Animal> GetAllAnimals() => _repo.Animals;
        public Task<IEnumerable<Animal>> GetAnimalsAsync() => Task.Run(() => GetAllAnimals());


        private Animal GetAnimalById(int id) => _repo.Animals!.FirstOrDefault(c => c.Id == id);
        public Task<Animal> GetAnimalByIdAsync(int id) => Task.Run(() => GetAnimalById(id));
        

        private IEnumerable<Animal> GetAnimalsByCategory(int categoryId) => _repo.Categories.FirstOrDefault(c => c.Id == categoryId).Animals!.ToList();
        public Task<IEnumerable<Animal>> GetAnimalsByCategoryAsync(int categoryId) => Task.Run(() => GetAnimalsByCategory(categoryId));


        private IEnumerable<Animal> GetTopCommentsAminals(int HowMany)
        {
            var res = _repo.Animals
                                .OrderByDescending(a => a.Comments!.Count)
                                .Take(HowMany)
                                .ToList();
            return res;
        }
        public Task<IEnumerable<Animal>> GetTopCommentsAminalsAsync(int HowMany) => Task.Run(() => GetTopCommentsAminals(HowMany));


        private void InsertAnimal(Animal animal) => _repo.InsertAnimal(animal);
        public Task InsertAnimalAsync(Animal animal) => Task.Run(() => InsertAnimal(animal));


        private void EditAnimal(Animal animal) => _repo.EditAnimal(animal);
        public Task EditAnimalAsync(Animal animal) => Task.Run(() => EditAnimal(animal));


        private void DeleteAnimal(int id) => _repo.DeleteAnimal(GetAnimalById(id));
        public Task DeleteAnimalAsync(int id) => Task.Run(() => DeleteAnimal(id));


        public bool IsNameAvailable(string name, int animalId = -1)
        {
            // For Edit Mode
            
            var animals = animalId != -1 ? 
                _repo.Animals.Where(anm =>  anm.Name == name && anm.Id != animalId).ToList() :
                _repo.Animals.Where(anm => anm.Name == name).ToList();

            return animals.Count == 0;
        }   
        public bool IsIdExist(int id) => _repo.Animals.Any(a => a.Id == id);
    }
}
