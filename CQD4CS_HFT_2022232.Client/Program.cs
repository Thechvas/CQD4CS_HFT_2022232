using CQD4CS_HFT_2022232.Repository.Database;
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

            ;
        }
    }
}
