using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework.Data
{
    internal class LibraryContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }  // таблица Authors
        public DbSet<Book> Books { get; set; }      // таблица Books

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;Database=LibraryDb;Trusted_Connection=True;"
            );
        }
    }
}
