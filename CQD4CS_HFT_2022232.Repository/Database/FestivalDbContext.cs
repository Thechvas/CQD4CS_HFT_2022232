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

            Festival lolla = new Festival() { Id = 1, Name = "Lollapalooza", Location = "Chicago", Duration = 4 };
            Festival sziget = new Festival() { Id = 2, Name = "Sziget Fesztivál", Location = "Budapest", Duration = 6 };
            Festival sound = new Festival() { Id = 3, Name = "Balaton Sound", Location = "Zamárdi", Duration = 4 };
            Festival tomorrowland = new Festival() { Id = 4, Name = "Tomorrowland", Location = "Boom", Duration = 6 };

            Artist ariana = new Artist() { Id = 1, FestivalId = lolla.Id, Name = "Ariana Grande", NumOfAlbums = 6 };
            Artist weeknd = new Artist() { Id = 2, FestivalId = lolla.Id, Name = "The Weeknd", NumOfAlbums = 5 };
            Artist em = new Artist() { Id = 3, FestivalId = lolla.Id, Name = "Eminem", NumOfAlbums = 11 };
            Artist dua = new Artist() { Id = 4, FestivalId = sziget.Id, Name = "Dua Lipa", NumOfAlbums = 2 };
            Artist sia = new Artist() { Id = 5, FestivalId = sziget.Id, Name = "Sia", NumOfAlbums = 9 };
            Artist illenium = new Artist() { Id = 6, FestivalId = sound.Id, Name = "Illenium", NumOfAlbums = 5 };
            Artist zedd = new Artist() { Id = 7, FestivalId = sound.Id, Name = "Zedd", NumOfAlbums = 2 };
            Artist garrix = new Artist() { Id = 8, FestivalId = tomorrowland.Id, Name = "Martin Garrix", NumOfAlbums = 1 };

            Song ari1 = new Song() { Id = 1, ArtistId = ariana.Id, Title = "imagine", Genre = "RnB", Length = 212 };
            Song ari2 = new Song() { Id = 2, ArtistId = ariana.Id, Title = "no tears left to cry", Genre = "Pop", Length = 205 };
            Song ari3 = new Song() { Id = 3, ArtistId = ariana.Id, Title = "breathin", Genre = "Pop", Length = 198 };
            Song weeknd1 = new Song() { Id = 4, ArtistId = weeknd.Id, Title = "Call Out My Name", Genre = "RnB", Length = 228 };
            Song weeknd2 = new Song() { Id = 5, ArtistId = weeknd.Id, Title = "Blinding Lights", Genre = "Pop", Length = 202 };
            Song em1 = new Song() { Id = 6, ArtistId = em.Id, Title = "Without Me", Genre = "Hip-Hop", Length = 290 };
            Song em2 = new Song() { Id = 7, ArtistId = em.Id, Title = "Lose Yourself", Genre = "Hip-Hop", Length = 322 };
            Song dua1 = new Song() { Id = 8, ArtistId = dua.Id, Title = "New Rules", Genre = "Pop", Length = 209 };
            Song dua2 = new Song() { Id = 9, ArtistId = dua.Id, Title = "Physical", Genre = "Pop", Length = 194 };
            Song sia1 = new Song() { Id = 10, ArtistId = sia.Id, Title = "Elastic Heart", Genre = "Pop", Length = 257 };
            Song illenium1 = new Song() { Id = 11, ArtistId = illenium.Id, Title = "Good Things Fall Apart", Genre = "Pop", Length = 216 };
            Song illenium2 = new Song() { Id = 12, ArtistId = illenium.Id, Title = "Nightlight", Genre = "Dance", Length = 222 };
            Song zedd1 = new Song() { Id = 13, ArtistId = zedd.Id, Title = "Stay", Genre = "EDM", Length = 210 };
            Song zedd2 = new Song() { Id = 14, ArtistId = zedd.Id, Title = "The Middle", Genre = "Dance", Length = 184};
            Song garrix1 = new Song() { Id = 15, ArtistId = garrix.Id, Title = "Animals", Genre = "EDM", Length = 184};
            Song garrix2 = new Song() { Id = 16, ArtistId = garrix.Id, Title = "There for You", Genre = "Pop", Length = 221};

            modelBuilder.Entity<Festival>().HasData(lolla, sziget, sound, tomorrowland);
            modelBuilder.Entity<Artist>().HasData(ariana, weeknd, em, dua, sia, illenium, zedd, garrix);
            modelBuilder.Entity<Song>().HasData(ari1, ari2, ari3, weeknd1, weeknd2, em1, em2, dua1, dua2, sia1, illenium1, illenium2, zedd1, zedd2, garrix1, garrix2);

        }
    }
}
