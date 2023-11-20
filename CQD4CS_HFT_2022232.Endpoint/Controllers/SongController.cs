using CQD4CS_HFT_2022232.Endpoint.Services;
using CQD4CS_HFT_2022232.Logic.Interfaces;
using CQD4CS_HFT_2022232.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CQD4CS_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        ISongLogic songLogic;
        IHubContext<SignalRHub> hub;


        public SongController(ISongLogic songLogic, IHubContext<SignalRHub> hub)
        {
            this.songLogic = songLogic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Song> ReadAll()
        {
            return this.songLogic.ReadAll();
        }

        [HttpGet("{id}")]
        public Song Read(int id)
        {
            return this.songLogic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Song value)
        {
            this.songLogic.Create(value);
            this.hub.Clients.All.SendAsync("SongCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Song value)
        {
            this.songLogic.Update(value);
            this.hub.Clients.All.SendAsync("SongUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var songToDelete = this.songLogic.Read(id);
            this.songLogic.Delete(id);
            this.hub.Clients.All.SendAsync("SongDeleted", songToDelete);
        }
    }

}
