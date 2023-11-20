using CQD4CS_HFT_2022232.Endpoint.Services;
using CQD4CS_HFT_2022232.Logic.Interfaces;
using CQD4CS_HFT_2022232.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;


namespace CQD4CS_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FestivalController : ControllerBase
    {
        IFestivalLogic festivalLogic;
        IHubContext<SignalRHub> hub;

        public FestivalController(IFestivalLogic festivalLogic, IHubContext<SignalRHub> hub)
        {
            this.festivalLogic = festivalLogic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Festival> ReadAll()
        {
            return this.festivalLogic.ReadAll();
        }

        [HttpGet("{id}")]
        public Festival Read(int id)
        {
            return this.festivalLogic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Festival value)
        {
            this.festivalLogic.Create(value);
            this.hub.Clients.All.SendAsync("FestivalCreated", value);

        }

        [HttpPut]
        public void Update([FromBody] Festival value)
        {
            this.festivalLogic.Update(value);
            this.hub.Clients.All.SendAsync("FestivalUpdated", value);

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var festivalToDelete = this.festivalLogic.Read(id);
            this.festivalLogic.Delete(id);
            this.hub.Clients.All.SendAsync("FestivalDeleted", festivalToDelete);
        }
    }

}
