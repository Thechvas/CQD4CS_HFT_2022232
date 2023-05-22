using CQD4CS_HFT_2022232.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQD4CS_HFT_2022232.Client
{
    public class NonCrudService
    {
        RestService rest;

        public NonCrudService(RestService rest)
        {
            this.rest = rest;
        }

        public void FestivalWithMostArtists()
        {
            var item = rest.GetSingle<string>("Stat/FestivalWithMostArtists");
            Console.WriteLine(item);
            Console.ReadLine();
        }

        public void ArtistWithMostAlbums()
        {
            Console.WriteLine("festivalLocation=");
            string festivalLocation = Console.ReadLine();

            var item = rest.GetSingle<string>($"Stat/ArtistWithMostAlbums?festivalLocation={festivalLocation}");
            Console.WriteLine(item);
            Console.ReadLine();
        }

        public void AlbumStatistics()
        {
            var items = rest.Get<AlbumInfo>("Stat/AlbumStatistics");
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }

        public void LongestSongOfArtist()
        {
            Console.WriteLine("artistName=");
            string artistName = Console.ReadLine();

            var item = rest.GetSingle<string>($"Stat/LongestSongOfArtist?artistName={artistName}");
            Console.WriteLine(item);
            Console.ReadLine();
        }

        public void ArtistStatistics()
        {
            var items = rest.Get<ArtistInfo>("Stat/ArtistStatistics");
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }

        public void TotalDurationOfFestival()
        {
            Console.WriteLine("festivalId=");
            int festivalId = int.Parse(Console.ReadLine());

            var item = rest.GetSingle<int>($"Stat/TotalDurationOfFestival?festivalId={festivalId}");
            Console.WriteLine(item + " seconds");
            Console.ReadLine();
        }

        public void SpecificSongFinder()
        {
            Console.WriteLine("artistName=");
            string artistName = Console.ReadLine();

            Console.WriteLine("genreName=");
            string genreName = Console.ReadLine();

            var item = rest.GetSingle<string>($"Stat/SpecificSongFinder?artistName={artistName}&genreName={genreName}");
            Console.WriteLine(item);
            Console.ReadLine();
        }
    }
}
