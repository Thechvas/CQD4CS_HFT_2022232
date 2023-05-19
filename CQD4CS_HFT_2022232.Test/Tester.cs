using CQD4CS_HFT_2022232.Logic.Interfaces;
using CQD4CS_HFT_2022232.Models;
using CQD4CS_HFT_2022232.Repository.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.ComponentModel;

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
        }

    }
}
