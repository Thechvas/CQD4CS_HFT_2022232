using CQD4CS_HFT_2022232.Logic.Classes;
using CQD4CS_HFT_2022232.Logic.Interfaces;
using CQD4CS_HFT_2022232.Models;
using CQD4CS_HFT_2022232.Models.DTOs;
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
                Festival = fakeFest1,
                NumOfAlbums = 1
            };
            Artist fakeArtist2 = new Artist()
            {
                Name = "Taylor Swift",
                Id = 2,
                Festival = fakeFest1,
                NumOfAlbums = 10
            };
            Artist fakeArtist3 = new Artist()
            {
                Name = "Kygo",
                Id = 3,
                Festival = fakeFest2,
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
                    Festival = fakeFest1,
                    NumOfAlbums = 1
                },
                new Artist()
                {
                    Name = "Taylor Swift",
                    Id = 2,
                    Festival = fakeFest1,
                    NumOfAlbums = 11
                },
                new Artist()
                {
                    Name = "Kygo",
                    Id = 3,
                    Festival = fakeFest2,
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
                    Artist = fakeArtist1,
                    Genre = "Pop",
                    Length = 209
                },
                new Song()
                {
                    Title = "Rewrite The Stars",
                    Id = 2,
                    Artist = fakeArtist1,
                    Genre = "Pop",
                    Length = 217
                },
                new Song()
                {
                    Title = "Lavender Haze",
                    Id = 3,
                    Artist = fakeArtist2,
                    Genre = "R&B",
                    Length = 202
                },
                new Song()
                {
                    Title = "Cruel Summer",
                    Id = 4,
                    Artist = fakeArtist2,
                    Genre = "Pop",
                    Length = 178
                },
                new Song()
                {
                    Title = "I Did Something Bad",
                    Id = 5,
                    Artist = fakeArtist2,
                    Genre = "Pop",
                    Length = 238
                },
                new Song()
                {
                    Title = "Stargazing",
                    Id = 6,
                    Artist = fakeArtist3,
                    Genre = "Dance",
                    Length = 236
                }
            }.AsQueryable());

            songLogic = new SongLogic(mockSongRepo.Object);
        }

        [Test]
        public void TotalDurationOfFestivalTester()
        {
            var result = songLogic.TotalDurationOfFestival(2);
            Assert.That(result, Is.EqualTo(236));
        }

        [Test]
        public void LongestSongOfArtistTester()
        {
            var result = songLogic.LongestSongOfArtist("Taylor Swift");
            Assert.That(result, Is.EqualTo("I Did Something Bad"));
        }

        [Test]
        public void FestivalWithMostArtistsTest()
        {
            var result = festivalLogic.FestivalWithMostArtists();
            Assert.That(result, Is.EqualTo("Volt Fesztivál"));   
        }

        [Test]
        public void AlbumStatistics_ReturnsValidStatistics()
        {
            var result = artistLogic.AlbumStatistics();
            var expected = new List<AlbumInfo>()
            {
                new AlbumInfo() { FestivalName = "Volt Fesztivál", ArtistCount = 2, AvgNumOfAlbums = 6 },
                new AlbumInfo(){ FestivalName = "EFOTT", ArtistCount = 1, AvgNumOfAlbums = 3}
            };
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GenreStatistics_ReturnsValidStatistics()
        {
            var result = songLogic.ArtistStatistics();
            var expected = new List<ArtistInfo>()
            {
                new ArtistInfo() { ArtistName = "Taylor Swift", SongNumber = 3, SumLengthOfSongs = 618 },
                new ArtistInfo() { ArtistName = "Zendaya", SongNumber = 2, SumLengthOfSongs = 426 },
                new ArtistInfo() { ArtistName = "Kygo", SongNumber = 1, SumLengthOfSongs = 236 }
            };
            Assert.That(result, Is.EqualTo(expected));
        }


    }
}
