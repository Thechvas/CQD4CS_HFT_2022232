using CQD4CS_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQD4CS_HFT_2022232.WpfClient
{
    public class MainWindowViewModel
    {
        public RestCollection<Artist> Artists { get; set; }
        public RestCollection<Song> Songs { get; set; }
        public RestCollection<Festival> Festivals { get; set; }

        public MainWindowViewModel()
        {
            Artists = new RestCollection<Artist>("http://localhost:36286/", "artist");
            Songs = new RestCollection<Song>("http://localhost:36286/", "song");
            Festivals = new RestCollection<Festival>("http://localhost:36286/", "festival");
        }
    }
}
