using CQD4CS_HFT_2022232.Logic.Interfaces;
using CQD4CS_HFT_2022232.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CQD4CS_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        IArtistLogic artistLogic;

        public ArtistController(IArtistLogic artistLogic)
        {
            this.artistLogic = artistLogic;
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
        }

        [HttpPut]
        public void Update([FromBody] Artist value)
        {
            this.artistLogic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.artistLogic.Delete(id);
        }
    }
}
