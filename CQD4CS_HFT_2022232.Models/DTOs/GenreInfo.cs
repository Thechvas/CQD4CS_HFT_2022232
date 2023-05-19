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

        public override string ToString()
        {
            return $"GenreName = {GenreName}, SongNumber = {SongNumber}, SumLenth = {SumLength}";
        }
    }
}
