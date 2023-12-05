using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class MyDotsDBContext : DbContext
    {
        public DbSet<MyDot> Dots { get; set; }
        public DbSet<MyComment> Comments { get; set; }

        public MyDotsDBContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "MyDotsDatabase");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region тестовые данные
            MyDot a = new MyDot { ID = 1, Cord_x = 100, Cord_y = 200, Radius = 15, ARGBColor = Color.DarkGray.ToArgb()};
            MyDot b = new MyDot { ID = 2, Cord_x = 300, Cord_y = 400, Radius = 30, ARGBColor = Color.Red.ToArgb()};
            MyComment a1 = new MyComment { ID = 1, Text = "Привет, мир!", MyDotID = 1, intcolor = Color.White.ToArgb() };
            MyComment a2 = new MyComment { ID = 2, Text = "Это тестовый комментарий.", MyDotID = 1, intcolor = Color.LightBlue.ToArgb() };
            MyComment b1 = new MyComment { ID = 3, Text = "Еще один комментарий.", MyDotID = 2, intcolor = Color.LawnGreen.ToArgb() };
            modelBuilder.Entity<MyDot>().HasData(a,b);            
            modelBuilder.Entity<MyComment>().HasData(a1,a2,b1);
            #endregion
            //один ко многим с каскадным удалением комментариев
            modelBuilder.Entity<MyDot>()
                .HasMany(e => e.Comments)
                .WithOne(e => e.Dot)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }

    }
}
