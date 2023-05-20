using CQD4CS_HFT_2022232.Logic.Interfaces;
using CQD4CS_HFT_2022232.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CQD4CS_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FestivalController : ControllerBase
    {
        IFestivalLogic festivalLogic;

        public FestivalController(IFestivalLogic festivalLogic)
        {
            this.festivalLogic = festivalLogic;
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
        }

        [HttpPut]
        public void Update([FromBody] Festival value)
        {
            this.festivalLogic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.festivalLogic.Delete(id);
        }
    }
}
