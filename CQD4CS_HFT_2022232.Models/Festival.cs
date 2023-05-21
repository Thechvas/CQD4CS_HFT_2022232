using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CQD4CS_HFT_2022232.Models
{
    public class Festival
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Duration { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Artist> Artists { get; set; }

        public Festival()
        {
            Artists = new HashSet<Artist>();
        }

        public override string ToString()
        {
            return $"#{Id}-FESTIVAL: Name = {Name}, Location = {Location}, Duartion = {Duration}";
        }
    }
}
