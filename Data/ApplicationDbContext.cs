using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using CimaLek.Models;
using Entertment.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CimaLek.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        DbSet<serie> series { get; set; }
        public DbSet<film> films { set; get; }
        public DbSet<filmSeriesType> filmSeriesTypes { set; get; }
        public DbSet<filmtype> filmtypes { set; get; }
        public DbSet<serieType> serieTypes { set; get; }
        public DbSet<filmWatch> filmWatches { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorToFilm> AuthorToFilms { get; set; }
        public DbSet<AuthorToSeries> AuthorToSeries { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<filmtype>().HasKey(ft => new { ft.filmId, ft.typeId });
            modelBuilder.Entity<filmtype>().HasOne(ft => ft.film).WithMany(f => f.filmtypes).HasForeignKey(ba => ba.filmId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<filmtype>().HasOne(ft => ft.type).WithMany(f => f.filmtypes).HasForeignKey(ba => ba.typeId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<serieType>().HasKey(ft => new { ft.serieId, ft.typeId });
            modelBuilder.Entity<serieType>().HasOne(ft => ft.serie).WithMany(f => f.serieTypes).HasForeignKey(ba => ba.serieId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<serieType>().HasOne(ft => ft.type).WithMany(f => f.serieTypes).HasForeignKey(ba => ba.typeId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AuthorToFilm>().HasKey(ft => new { ft.filmId, ft.authorId });
            modelBuilder.Entity<AuthorToFilm>().HasOne(ft => ft.film).WithMany(f => f.AuthorToFilms).HasForeignKey(ba => ba.filmId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<AuthorToFilm>().HasOne(ft => ft.Author).WithMany(f => f.AuthorToFilms).HasForeignKey(ba => ba.authorId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AuthorToSeries>().HasKey(ft => new { ft.serieId, ft.authorId });
            modelBuilder.Entity<AuthorToSeries>().HasOne(ft => ft.serie).WithMany(f => f.AuthorToSeries).HasForeignKey(ba => ba.serieId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<AuthorToSeries>().HasOne(ft => ft.Author).WithMany(f => f.AuthorToSeries).HasForeignKey(ba => ba.authorId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<filmWatch>().Ignore(x => x.Again);


            modelBuilder.Entity<Author>().Ignore(x => x.Image);
            modelBuilder.Entity<Author>().Ignore(x => x.Again);

            modelBuilder.Entity<filmSeriesType>().HasData(
               new filmSeriesType { typeID=1,type="aomance" },
               new filmSeriesType { typeID = 2, type = "action" },
               new filmSeriesType { typeID = 3, type = "animation" },
               new filmSeriesType { typeID = 4, type = "drama" },
               new filmSeriesType { typeID = 5, type = "comedy" },
               new filmSeriesType { typeID = 6, type = "history" },
               new filmSeriesType { typeID = 7, type = "war" },
               new filmSeriesType { typeID = 8, type = "CV" },
               new filmSeriesType { typeID = 9, type = "mystery" },
               new filmSeriesType { typeID = 10, type = "murder" },
               new filmSeriesType { typeID = 11, type = "fiction" },
               new filmSeriesType { typeID = 12, type = "scary" },
               new filmSeriesType { typeID = 14, type = "sport" },
               new filmSeriesType { typeID = 15, type = "adventure" },
               new filmSeriesType { typeID = 16, type = "musical " },
               new filmSeriesType { typeID = 17, type = "documentary " }
           );
            

        }
        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager= serviceProvider.GetService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            string userName = "AbdoAhmed";
            string email = "abdoo.ahmed38@icloud.com";
            string password = "Samara.24121977";
            string role = "Admin";
            if (await roleManager.FindByNameAsync(role) == null)
                await roleManager.CreateAsync(new IdentityRole(role));
            if(await userManager.FindByNameAsync(userName)==null)
            {
                var user = new User { UserName = userName, Email = email };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
        public DbSet<Entertment.Models.serie> serie { get; set; }

    }
}
