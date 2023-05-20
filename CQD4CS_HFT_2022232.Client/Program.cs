using CQD4CS_HFT_2022232.Logic.Classes;
using CQD4CS_HFT_2022232.Repository.Database;
using CQD4CS_HFT_2022232.Repository.ModelRepositories;
using System;
using System.Linq;

namespace CQD4CS_HFT_2022232.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FestivalDbContext db = new FestivalDbContext();
            var festivals = db.Festivals.ToArray();
            var artists = db.Artists.ToArray();
            var songs = db.Songs.ToArray();

            var repo = new FestivalRepository(db);
            var repo1 = new ArtistRepository(db);
            var repo2 = new SongRepository(db);
            var flogic = new FestivalLogic(repo);
            var alogic = new ArtistLogic(repo1);
            var slogic = new SongLogic(repo2);

            var items = flogic.ReadAll();
            var nc1 = slogic.TotalDurationOfFestival(1);
            var nc2 = flogic.FestivalWithMostArtists();
            var nc3 = slogic.LongestSongOfArtist("Eminem");
            var nc4 = alogic.AlbumStatistics().ToArray();
            var nc5 = slogic.ArtistStatistics().ToArray();
            var nc6 = slogic.SpecificSongFinder("Ariana Grande", "R&B");
            var nc7 = alogic.ArtistWithMostAlbums("Budapest");

            ;
        }
    }
}
