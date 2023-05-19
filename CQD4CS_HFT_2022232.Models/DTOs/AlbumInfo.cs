using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQD4CS_HFT_2022232.Models.DTOs
{
    public class AlbumInfo
    {
        public string FestivalName { get; set; }
        public int ArtistCount { get; set; }
        public double AvgNumOfAlbums { get; set; }

        public AlbumInfo()
        {
            
        }

        public override string ToString()
        {
            return $"FestivalName = {FestivalName}, ArtistCount = {ArtistCount}, AvgNumOfAlbums = {AvgNumOfAlbums}";
        }
    }
}
