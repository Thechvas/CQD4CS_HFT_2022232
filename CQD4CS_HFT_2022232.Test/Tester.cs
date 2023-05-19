using CQD4CS_HFT_2022232.Logic.Classes;
using CQD4CS_HFT_2022232.Logic.Interfaces;
using CQD4CS_HFT_2022232.Models;
using CQD4CS_HFT_2022232.Repository.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CQD4CS_HFT_2022232.Test
{
    [TestFixture]
    public class Tester
    {
        IFestivalLogic festivalLogic;
        IArtistLogic artistLogic;
        ISongLogic songLogic;

        Mock<IRepository<Festival>> mockFestivalRepo;
        Mock<IRepository<Artist>> mockArtistRepo;
        Mock<IRepository<Song>> mockSongRepo;

        [SetUp]
        public void Init()
        {
            mockFestivalRepo = new Mock<IRepository<Festival>>();
            mockArtistRepo = new Mock<IRepository<Artist>>();
            mockSongRepo = new Mock<IRepository<Song>>();

            Festival fakeFest1 = new Festival()
            {
                Name = "Volt Fesztivál",
                Id = 1,
                Location = "Sopron",
                Duration = 4
            };
            Festival fakeFest2 = new Festival()
            {
                Name = "EFOTT",
                Id = 2,
                Location = "Sukoró",
                Duration = 5
            };

            Artist fakeArtist1 = new Artist()
            {
                Name = "Zendaya",
                Id = 1,
                FestivalId = fakeFest1.Id,
                NumOfAlbums = 1
            };
            Artist fakeArtist2 = new Artist()
            {
                Name = "Taylor Swift",
                Id = 2,
                FestivalId = fakeFest1.Id,
                NumOfAlbums = 10
            };
            Artist fakeArtist3 = new Artist()
            {
                Name = "Kygo",
                Id = 3,
                FestivalId = fakeFest2.Id,
                NumOfAlbums = 3,
            };

            mockFestivalRepo.Setup((t) => t.Create(It.IsAny<Festival>()));
            mockFestivalRepo.Setup((t) => t.ReadAll()).Returns(
            new List<Festival>()
            {
                new Festival()
                {
                    Name = "Volt Fesztivál",
                    Id = 1,
                    Location = "Sopron",
                    Duration = 4
                },
                new Festival()
                {
                    Name = "EFOTT",
                    Id = 2,
                    Location = "Sukoró",
                    Duration = 5
                }
            }.AsQueryable());

            festivalLogic = new FestivalLogic(mockFestivalRepo.Object);


            mockArtistRepo.Setup((t) => t.Create(It.IsAny<Artist>()));
            mockArtistRepo.Setup((t) => t.ReadAll()).Returns(
            new List<Artist>()
            {
                new Artist()
                {
                    Name = "Zendaya",
                    Id = 1,
                    FestivalId = fakeFest1.Id,
                    NumOfAlbums = 1
                },
                new Artist()
                {
                    Name = "Taylor Swift",
                    Id = 2,
                    FestivalId = fakeFest1.Id,
                    NumOfAlbums = 10
                },
                new Artist()
                {
                    Name = "Kygo",
                    Id = 3,
                    FestivalId = fakeFest2.Id,
                    NumOfAlbums = 3
                }
            }.AsQueryable());

            artistLogic = new ArtistLogic(mockArtistRepo.Object);

            mockSongRepo.Setup((t) => t.Create(It.IsAny<Song>()));
            mockSongRepo.Setup((t) => t.ReadAll()).Returns(
            new List<Song>()
            {
                new Song()
                {
                    Title = "Replay",
                    Id = 1,
                    ArtistId = fakeArtist1.Id,
                    Genre = "Pop",
                    Length = 209
                },
                new Song()
                {
                    Title = "Rewrite The Stars",
                    Id = 2,
                    ArtistId = fakeArtist1.Id,
                    Genre = "Pop",
                    Length = 217
                },
                new Song()
                {
                    Title = "Lavender Haze",
                    Id = 3,
                    ArtistId = fakeArtist2.Id,
                    Genre = "R&B",
                    Length = 202
                },
                new Song()
                {
                    Title = "Cruel Summer",
                    Id = 4,
                    ArtistId = fakeArtist2.Id,
                    Genre = "Pop",
                    Length = 178
                },
                new Song()
                {
                    Title = "I Did Something Bad",
                    Id = 5,
                    ArtistId = fakeArtist2.Id,
                    Genre = "Pop",
                    Length = 238
                },
                new Song()
                {
                    Title = "Stargazing",
                    Id = 6,
                    ArtistId = fakeArtist3.Id,
                    Genre = "Dance",
                    Length = 236
                }
            }.AsQueryable());

            songLogic = new SongLogic(mockSongRepo.Object);
        }

    }
}
