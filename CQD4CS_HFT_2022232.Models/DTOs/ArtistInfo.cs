using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQD4CS_HFT_2022232.Models.DTOs
{
    public class ArtistInfo
    {
        public string ArtistName { get; set; }
        public int SongNumber { get; set; }
        public double SumLengthOfSongs { get; set; }

        public ArtistInfo()
        {

        }
        public override bool Equals(object obj)
        {
            var other = obj as ArtistInfo;
            return ArtistName == other.ArtistName && SongNumber == other.SongNumber && SumLengthOfSongs == other.SumLengthOfSongs;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.ArtistName, this.SongNumber, this.SumLengthOfSongs);
        }

        public override string ToString()
        {
            return $"ArtistName = {ArtistName}, SongNumber = {SongNumber}, SumLengthOfSongs = {SumLengthOfSongs}";
        }


    }
}
