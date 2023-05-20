using CQD4CS_HFT_2022232.Logic.Interfaces;
using CQD4CS_HFT_2022232.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CQD4CS_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        ISongLogic songLogic;

        public SongController(ISongLogic songLogic)
        {
            this.songLogic = songLogic;
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
        }

        [HttpPut]
        public void Update([FromBody] Song value)
        {
            this.songLogic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.songLogic.Delete(id);
        }
    }

}
