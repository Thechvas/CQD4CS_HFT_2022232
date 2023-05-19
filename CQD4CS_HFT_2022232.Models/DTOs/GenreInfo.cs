using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQD4CS_HFT_2022232.Models.DTOs
{
    public class GenreInfo
    {
        public string GenreName { get; set; }
        public int SongNumber { get; set; }
        public double SumLength { get; set; }

        public GenreInfo()
        {

        }
        public override bool Equals(object obj)
        {
            var other = obj as GenreInfo;
            return GenreName == other.GenreName && SongNumber == other.SongNumber && SumLength == other.SumLength;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.GenreName, this.SongNumber, this.SumLength);
        }

        public override string ToString()
        {
            return $"GenreName = {GenreName}, SongNumber = {SongNumber}, SumLength = {SumLength}";
        }


    }
}
