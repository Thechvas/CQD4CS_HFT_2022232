using CQD4CS_HFT_2022232.Models;
using CQD4CS_HFT_2022232.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQD4CS_HFT_2022232.Logic.Interfaces
{
    public interface IArtistLogic
    {
        void Create(Artist item);
        void Delete(int id);
        Artist Read(int id);
        IQueryable<Artist> ReadAll();
        void Update(Artist item);
        public IEnumerable<AlbumInfo> AlbumStatistics();
        public string ArtistWithMostAlbums(string festivalLocation);
    }
}
