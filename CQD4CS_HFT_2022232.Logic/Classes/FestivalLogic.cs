using CQD4CS_HFT_2022232.Logic.Interfaces;
using CQD4CS_HFT_2022232.Models;
using CQD4CS_HFT_2022232.Models.DTOs;
using CQD4CS_HFT_2022232.Repository.Interfaces;
using CQD4CS_HFT_2022232.Repository.ModelRepositories;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CQD4CS_HFT_2022232.Logic.Classes
{
    public class FestivalLogic : IFestivalLogic
    {
        IRepository<Festival> repo;
        public FestivalLogic(IRepository<Festival> repo)
        {
            this.repo = repo;
        }

        public void Create(Festival item)
        {
            if (item.Duration < 2) 
            {
                throw new ArgumentException("A festivals duration must be at least 2 days long!");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Festival Read(int id)
        {
            var festival = this.repo.Read(id);
            if (festival == null)
            {
                throw new ArgumentException("Festival does not exist");
            }
            return festival;
        }

        public IQueryable<Festival> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Festival item)
        {
            this.repo.Update(item);
        }

        //non cruds

        //Total duration of a festival by summing the lengths of all songs performed by its artists
        public int TotalDurationOfFestival(int festivalId)
        {
            return this.repo.ReadAll()
                            .Where(festival => festival.Id == festivalId)
                            .SelectMany(festival => festival.Artists)
                            .SelectMany(artist => artist.Songs)
                            .Sum(song => song.Length);
        }

        //Festival with the most artists
        public string FestivalWithMostArtists()
        {
            return this.repo.ReadAll()
                            .OrderByDescending(festival => festival.Artists.Count)
                            .FirstOrDefault().Name;
                            
        }

        //Longest song per artist
        public string LongestSongOfArtist(string artistName)
        {
            return this.repo.ReadAll()
                            .SelectMany(festival => festival.Artists)
                            .Where(artist => artist.Name == artistName)
                            .SelectMany (artist => artist.Songs)
                            .OrderByDescending(song => song.Length)
                            .FirstOrDefault().Title;
        }

        //Festivals with its number of artists and their average number of albums 
        public IEnumerable<AlbumInfo> AlbumStatistics()
        {
            return from x in this.repo.ReadAll().SelectMany(t => t.Artists)
                   group x by x.Festival.Name into grp
                   select new AlbumInfo()
                   {
                       FestivalName = grp.Key,
                       ArtistCount = grp.Count(),
                       AvgNumOfAlbums = grp.Average(n => n.NumOfAlbums)
                   };
        }

        //Genres, number of songs in genres, summarized lenghts of songs in genres
        public IEnumerable<GenreInfo> GenreStatistics()
        {
            return from x in this.repo.ReadAll().SelectMany(t => t.Artists).SelectMany(z => z.Songs)
                   group x by x.Genre into grp
                   orderby grp.Sum(y => y.Length) descending
                   select new GenreInfo()
                   {
                       GenreName = grp.Key,
                       SongNumber = grp.Count(),
                       SumLength = grp.Sum(n => n.Length)
                   };
        }
    }
}
