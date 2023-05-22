using CQD4CS_HFT_2022232.Logic.Interfaces;
using CQD4CS_HFT_2022232.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CQD4CS_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IFestivalLogic festivalLogic;
        IArtistLogic artistLogic;
        ISongLogic songLogic;

        public StatController(IFestivalLogic festivalLogic, IArtistLogic artistLogic, ISongLogic songLogic)
        {
            this.festivalLogic = festivalLogic;
            this.artistLogic = artistLogic;
            this.songLogic = songLogic;
        }

        [HttpGet]
        public string FestivalWithMostArtists()
        {
            return festivalLogic.FestivalWithMostArtists();
        }

        [HttpGet]
        public string ArtistWithMostAlbums([FromQuery] string festivalLocation)
        {
            return artistLogic.ArtistWithMostAlbums(festivalLocation);
        }

        [HttpGet]
        public IEnumerable<AlbumInfo> AlbumStatistics()
        {
            return artistLogic.AlbumStatistics();
        }

        [HttpGet]
        public string LongestSongOfArtist([FromQuery] string artistName)
        {
            return songLogic.LongestSongOfArtist(artistName);
        }

        [HttpGet]
        public IEnumerable<ArtistInfo> ArtistStatistics()
        {
            return songLogic.ArtistStatistics();
        }

        [HttpGet]
        public int TotalDurationOfFestival([FromQuery] int festivalId)
        {
            return songLogic.TotalDurationOfFestival(festivalId);
        }

        [HttpGet]
        public string SpecificSongFinder([FromQuery] string artistName, string genreName)
        {
            return songLogic.SpecificSongFinder(artistName, genreName);
        }
    }
}
