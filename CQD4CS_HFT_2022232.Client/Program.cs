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
            var logic = new FestivalLogic(repo);

            var items = logic.ReadAll();
            var nc1 = logic.TotalDurationOfFestival(1);
            var nc2 = logic.FestivalWithMostArtists();
            var nc3 = logic.LongestSongOfArtist("Eminem");
            var nc4 = logic.AlbumStatistics().ToArray();
            var nc5 = logic.GenreStatistics().ToArray();

            ;
        }
    }
}
