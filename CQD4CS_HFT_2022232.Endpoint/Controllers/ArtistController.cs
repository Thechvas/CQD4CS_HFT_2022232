using CQD4CS_HFT_2022232.Endpoint.Services;
using CQD4CS_HFT_2022232.Logic.Interfaces;
using CQD4CS_HFT_2022232.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CQD4CS_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        IArtistLogic artistLogic;
        IHubContext<SignalRHub> hub;

        public ArtistController(IArtistLogic artistLogic, IHubContext<SignalRHub> hub)
        {
            this.artistLogic = artistLogic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Artist> ReadAll()
        {
            return this.artistLogic.ReadAll();
        }

        [HttpGet("{id}")]
        public Artist Read(int id)
        {
            return this.artistLogic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Artist value)
        {
            this.artistLogic.Create(value);
            this.hub.Clients.All.SendAsync("ArtistCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Artist value)
        {
            this.artistLogic.Update(value);
            this.hub.Clients.All.SendAsync("ArtistUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var artistToDelete = this.artistLogic.Read(id);
            this.artistLogic.Delete(id);
            this.hub.Clients.All.SendAsync("ArtistDeleted", artistToDelete);
        }
    }


}
