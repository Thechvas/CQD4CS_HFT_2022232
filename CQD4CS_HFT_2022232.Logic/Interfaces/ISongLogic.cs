using CQD4CS_HFT_2022232.Models;
using CQD4CS_HFT_2022232.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQD4CS_HFT_2022232.Logic.Interfaces
{
    public interface ISongLogic
    {
        void Create(Song item);
        void Delete(int id);
        Song Read(int id);
        IQueryable<Song> ReadAll();
        void Update(Song item);
        public string LongestSongOfArtist(string artistName);
        public IEnumerable<ArtistInfo> ArtistStatistics();
        public int TotalDurationOfFestival(int festivalId);
        public string SpecificSongFinder(string artistName, string genreName);
    }
}
