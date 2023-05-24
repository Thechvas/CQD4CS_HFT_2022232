using CQD4CS_HFT_2022232.Logic.Interfaces;
using CQD4CS_HFT_2022232.Models;
using CQD4CS_HFT_2022232.Models.DTOs;
using CQD4CS_HFT_2022232.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQD4CS_HFT_2022232.Logic.Classes
{
    public class SongLogic : ISongLogic
    {
        IRepository<Song> repo;
        public SongLogic(IRepository<Song> repo)
        {
            this.repo = repo;
        }

        public void Create(Song item)
        {
            if (item.Length < 0)
            {
                throw new ArgumentException("Length of songs cannot be negative!");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Song Read(int id)
        {
            var song = this.repo.Read(id);
            if (song == null)
            {
                throw new ArgumentException("Song does not exist");
            }
            return song;
        }

        public IQueryable<Song> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Song item)
        {
            if (item.Length < 0)
            {
                throw new ArgumentException("Length of songs cannot be negative!");
            }
            this.repo.Update(item);
        }

        //Longest song per artist
        public string LongestSongOfArtist(string artistName)
        {
            return this.repo.ReadAll()
                .Where(song => song.Artist.Name == artistName)
                .OrderByDescending(song => song.Length)
                .FirstOrDefault().Title;
        }

        //Artists, number of their songs, summarized lenghts of their songs
        public IEnumerable<ArtistInfo> ArtistStatistics()
        {
            return from x in this.repo.ReadAll()
                   group x by x.Artist.Name into grp
                   orderby grp.Sum(y => y.Length) descending
                   select new ArtistInfo()
                   {
                       ArtistName = grp.Key,
                       SongNumber = grp.Count(),
                       SumLengthOfSongs = grp.Sum(n => n.Length)
                   };
        }

        //Total duration of a festival by summing the lengths of all songs performed by its artists
        public int TotalDurationOfFestival(int festivalId)
        {
            return this.repo.ReadAll()
                            .Where(song => song.Artist.Festival.Id == festivalId)
                            .Sum(song => song.Length);
        }

        //Finds a song from a specific artist in a specific genre
        public string SpecificSongFinder(string artistName, string genreName)
        {
            return this.repo.ReadAll()
                .Where(t => t.Artist.Name == artistName)
                .Where(z => z.Genre == genreName)
                .FirstOrDefault().Title;
        }
    }
}
