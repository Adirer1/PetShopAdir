using Microsoft.EntityFrameworkCore;
using PetShopAdir.Models;
using System;

namespace PetShopAdir.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            var categories = new List<Category>
    {
        new Category { Id = 1, Name = "Mammal" },
        new Category { Id = 2, Name = "Reptile" },
        new Category { Id = 3, Name = "Fish" },
        new Category { Id = 4, Name = "Bird" },
        new Category { Id = 5, Name = "Insect" }
    };

            modelBuilder.Entity<Category>().HasData(categories);

           
            var animals = new List<Animal>
    {
        new Animal { Id = 1, Name = "Dog", Age = 3, PictureName = $"/images/dog.jpg", Description = "A loyal pet dog.", CategoryId = 1 },
        new Animal { Id = 2, Name = "Cat", Age = 2, PictureName = $"/images/cat.jpg", Description = "A playful pet cat.", CategoryId = 1 },
        new Animal { Id = 3, Name = "Rabbit", Age = 1, PictureName = $"/images/rabbit.jpg", Description = "A cute pet rabbit.", CategoryId = 1 },
        new Animal { Id = 4, Name = "Turtle", Age = 5, PictureName = $"/images/turtle.jpg", Description = "A pet turtle.", CategoryId = 2 },
        new Animal { Id = 5, Name = "Gecko", Age = 4, PictureName = $"/images/gecko.jpg", Description = "A pet gecko.", CategoryId = 2 },
        new Animal { Id = 6, Name = "Iguana", Age = 6, PictureName = $"/images/iguana.jpg", Description = "A pet iguana.", CategoryId = 2 },
        new Animal { Id = 7, Name = "Goldfish", Age = 1, PictureName = $"/images/goldfish.jpg", Description = "A pet goldfish.", CategoryId = 3 },
        new Animal { Id = 8, Name = "Betta Fish", Age = 2, PictureName = $"/images/betta_fish.jpg", Description = "A colorful betta fish.", CategoryId = 3 },
        new Animal { Id = 9, Name = "Parrot", Age = 4, PictureName = $"/images/parrot.jpg", Description = "A talkative pet parrot.", CategoryId = 4 },
        new Animal { Id = 10, Name = "Butterfly", Age = 0, PictureName = $"/images/butterfly.jpg", Description = "A beautiful butterfly.", CategoryId = 5 },
        new Animal { Id = 11, Name = "Ladybug", Age = 0, PictureName = $"/images/ladybug.jpg", Description = "A cute ladybug.", CategoryId = 5 },
        new Animal { Id = 12, Name = "Ant", Age = 0, PictureName = $"/images/ant.jpg", Description = "A tiny ant.", CategoryId = 5 }
    };

            modelBuilder.Entity<Animal>().HasData(animals);

           
            var comments = new List<Comment>
    {
        new Comment { Id = 1, CommentText = "Great pet!", AnimalId = 1 },
        new Comment { Id = 2, CommentText = "I love my cat.", AnimalId = 2 },
        new Comment { Id = 3, CommentText = "Our rabbit is adorable.", AnimalId = 3 },
        new Comment { Id = 4, CommentText = "Our turtle is so friendly.", AnimalId = 4 },
        new Comment { Id = 5, CommentText = "Geckos are fascinating.", AnimalId = 5 },
        new Comment { Id = 6, CommentText = "Iguanas are cool reptiles.", AnimalId = 6 },
        new Comment { Id = 7, CommentText = "Our goldfish is beautiful.", AnimalId = 7 },
        new Comment { Id = 8, CommentText = "Betta fish are colorful.", AnimalId = 8 },
        new Comment { Id = 9, CommentText = "Our parrot talks a lot.", AnimalId = 9 },
        new Comment { Id = 10, CommentText = "Butterflies are lovely insects.", AnimalId = 10 },
        new Comment { Id = 11, CommentText = "Ladybugs bring good luck.", AnimalId = 11 },
        new Comment { Id = 12, CommentText = "Ants are hardworking insects.", AnimalId = 12 },
        new Comment { Id = 13, CommentText = "Dogs are the best!", AnimalId = 1 },
        new Comment { Id = 14, CommentText = "Cats are awesome", AnimalId = 2 },
         new Comment { Id = 15, CommentText = "He is so loyal...", AnimalId = 1 }
    };

            modelBuilder.Entity<Comment>().HasData(comments);
        }

     
    }
}

