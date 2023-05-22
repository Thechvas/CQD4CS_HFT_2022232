using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CQD4CS_HFT_2022232.Models
{
    public class Song
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Length { get; set; }

        [ForeignKey(nameof(Artist))]
        public int ArtistId { get; set; }
        [NotMapped]
        public virtual Artist Artist { get; set; }

        public override string ToString()
        {
            return $"#{Id}-SONG: Title = {Title}, Genre = {Genre}, Length = {Length} seconds, ArtistId = {ArtistId}";
        }
    }
}
