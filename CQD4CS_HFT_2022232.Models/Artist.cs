using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CQD4CS_HFT_2022232.Models
{
    public class Artist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfAlbums { get; set; }

        [ForeignKey(nameof(Festival))]
        public int FestivalId { get; set; }
        [NotMapped]
        public virtual Festival Festival { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Song> Songs { get; set; }

        public Artist()
        {
            Songs = new HashSet<Song>();
        }

        public override string ToString()
        {
            return $"#{Id}-ARTIST: Name = {Name}, Number of albums = {NumOfAlbums}, FestivalId = {FestivalId}";
        }
    }
}
