using CQD4CS_HFT_2022232.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CQD4CS_HFT_2022232.Repository.Database
{
    public class FestivalDbContext : DbContext
    {
        public DbSet<Festival> Festivals { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }

        public FestivalDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies ()
                    .UseInMemoryDatabase("festivalDb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>(entity =>
            {
                entity
                .HasOne(artist => artist.Festival)
                .WithMany(festival => festival.Artists)
                .HasForeignKey(artist => artist.FestivalId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity
                .HasOne(song => song.Artist)
                .WithMany(artist => artist.Songs)
                .HasForeignKey(song => song.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}
