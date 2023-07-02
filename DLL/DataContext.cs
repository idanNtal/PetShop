using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using WebAppProject.Models;

namespace WebAppProject.DAL
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<Animal>? Animals { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Comment>? Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Category mammals = new Category { Id = 1, Name = "Mammals" };
            Category reptiles = new Category { Id = 2, Name = "Reptiles" };
            Category birds = new Category { Id = 3, Name = "Birds" };
            modelBuilder.Entity<Category>().HasData(
                mammals,
                reptiles,
                birds
            );

            modelBuilder.Entity<Animal>().HasData(
                new Animal { Id = 1, Name = "Dog", Age = 5, Description = "A domesticated carnivorous mammal.", CategoryId = 1, PictureName = "Dog.png" },
                new Animal { Id = 2, Name = "Cat", Age = 3, Description = "A small domesticated carnivorous mammal.", CategoryId = 1, PictureName = "Cat.png" },
                new Animal { Id = 3, Name = "Elephant", Age = 10, Description = "A large herbivorous mammal with a long trunk.", CategoryId = 1, PictureName = "Elephant.png" },
                new Animal { Id = 4, Name = "Dolphin", Age = 8, Description = "A highly intelligent aquatic mammal.", CategoryId = 1, PictureName = "Dolphin.png" },

                new Animal { Id = 5, Name = "Snake", Age = 2, Description = "A long, legless reptile.", CategoryId = 2, PictureName = "Snake.png" },
                new Animal { Id = 6, Name = "Lizard", Age = 1, Description = "A scaly reptile with four legs.", CategoryId = 2, PictureName = "Lizard.png" },
                new Animal { Id = 7, Name = "Turtle", Age = 7, Description = "A reptile with a protective shell.", CategoryId = 2, PictureName = "Turtle.png" },
                new Animal { Id = 8, Name = "Alligator", Age = 12, Description = "A large aquatic reptile with a broad snout.", CategoryId = 2, PictureName = "Alligator.png" },

                new Animal { Id = 9, Name = "Sparrow", Age = 2, Description = "A small bird with brown feathers.", CategoryId = 3, PictureName = "Sparrow.png" },
                new Animal { Id = 10, Name = "Eagle", Age = 6, Description = "A large bird of prey with excellent eyesight.", CategoryId = 3, PictureName = "Eagle.png" },
                new Animal { Id = 11, Name = "Penguin", Age = 4, Description = "A flightless bird with a black and white plumage.", CategoryId = 3, PictureName = "Penguin.png" },
                new Animal { Id = 12, Name = "Parrot", Age = 8, Description = "A colorful bird known for its ability to mimic sounds.", CategoryId = 3, PictureName = "Parrot.png" }
            );

            modelBuilder.Entity<Comment>().HasData(
                new Comment { Id = 1, AnimalId = 1, comment = "Dogs are such loyal companions.", UserId = "4da939ba-1b80-4f2c-b6a1-9aa5809cee3d" },
                new Comment { Id = 2, AnimalId = 1, comment = "I love playing fetch with my dog.", UserId = "4da939ba-1b80-4f2c-b6a1-9aa5809cee3d" },
                new Comment { Id = 3, AnimalId = 2, comment = "Cats are independent and adorable.", UserId = "4da939ba-1b80-4f2c-b6a1-9aa5809cee3d" },
                new Comment { Id = 4, AnimalId = 2, comment = "I enjoy cuddling with my cat.", UserId = "4da939ba-1b80-4f2c-b6a1-9aa5809cee3d" },
                new Comment { Id = 5, AnimalId = 3, comment = "Elephants are magnificent creatures.", UserId = "4da939ba-1b80-4f2c-b6a1-9aa5809cee3d" },
                new Comment { Id = 6, AnimalId = 3, comment = "Their trunks are so versatile.", UserId = "4da939ba-1b80-4f2c-b6a1-9aa5809cee3d" },
                new Comment { Id = 7, AnimalId = 4, comment = "Dolphins are incredibly intelligent.", UserId = "4da939ba-1b80-4f2c-b6a1-9aa5809cee3d" },
                new Comment { Id = 8, AnimalId = 4, comment = "I'm amazed by their acrobatic skills.", UserId = "4da939ba-1b80-4f2c-b6a1-9aa5809cee3d" },
                new Comment { Id = 9, AnimalId = 5, comment = "Snakes are fascinating reptiles." , UserId = "4da939ba-1b80-4f2c-b6a1-9aa5809cee3d" },
                new Comment { Id = 10, AnimalId = 6, comment = "Lizards make interesting pets." , UserId = "4da939ba-1b80-4f2c-b6a1-9aa5809cee3d" },
                new Comment { Id = 11, AnimalId = 6, comment = "I find lizards mesmerizing." , UserId = "4da939ba-1b80-4f2c-b6a1-9aa5809cee3d" },
                new Comment { Id = 12, AnimalId = 6, comment = "Their ability to regrow their tails is impressive." , UserId = "4da939ba-1b80-4f2c-b6a1-9aa5809cee3d" },
                new Comment { Id = 13, AnimalId = 7, comment = "Turtles are so cute!" , UserId = "4da939ba-1b80-4f2c-b6a1-9aa5809cee3d" },
                new Comment { Id = 14, AnimalId = 7, comment = "I love their shells." , UserId = "4da939ba-1b80-4f2c-b6a1-9aa5809cee3d" },
                new Comment { Id = 15, AnimalId = 8, comment = "Alligators are fascinating creatures." , UserId = "4da939ba-1b80-4f2c-b6a1-9aa5809cee3d" },
                new Comment { Id = 16, AnimalId = 9, comment = "Sparrows sing beautifully." , UserId = "4da939ba-1b80-4f2c-b6a1-9aa5809cee3d" },
                new Comment { Id = 17, AnimalId = 9, comment = "I enjoy watching them fly." , UserId = "4da939ba-1b80-4f2c-b6a1-9aa5809cee3d" },
                new Comment { Id = 18, AnimalId = 10, comment = "Eagles are majestic creatures." , UserId = "4da939ba-1b80-4f2c-b6a1-9aa5809cee3d" },
                new Comment { Id = 19, AnimalId = 10, comment = "Their hunting skills are impressive." , UserId = "4da939ba-1b80-4f2c-b6a1-9aa5809cee3d" },
                new Comment { Id = 20, AnimalId = 10, comment = "I wish I could see an eagle up close." , UserId = "4da939ba-1b80-4f2c-b6a1-9aa5809cee3d" },
                new Comment { Id = 21, AnimalId = 11, comment = "Penguins are adorable!" , UserId = "4da939ba-1b80-4f2c-b6a1-9aa5809cee3d" },
                new Comment { Id = 22, AnimalId = 11, comment = "I love their waddling walk." , UserId = "4da939ba-1b80-4f2c-b6a1-9aa5809cee3d" }
                //new Comment { Id = 23, AnimalId = 12, comment = "Parrots are so talkative!" , UserId = "4da939ba-1b80-4f2c-b6a1-9aa5809cee3d" },
                //new Comment { Id = 24, AnimalId = 12, comment = "I wish I had a pet parrot." , UserId = "4da939ba-1b80-4f2c-b6a1-9aa5809cee3d" }
            );
        }
    }
}