using CQD4CS_HFT_2022232.Logic.Interfaces;
using CQD4CS_HFT_2022232.Models;
using CQD4CS_HFT_2022232.Models.DTOs;
using CQD4CS_HFT_2022232.Repository.Interfaces;
using CQD4CS_HFT_2022232.Repository.ModelRepositories;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CQD4CS_HFT_2022232.Logic.Classes
{
    public class FestivalLogic : IFestivalLogic
    {
        IRepository<Festival> repo;
        public FestivalLogic(IRepository<Festival> repo)
        {
            this.repo = repo;
        }

        public void Create(Festival item)
        {
            if (item.Duration < 2) 
            {
                throw new ArgumentException("A festivals duration must be at least 2 days long!");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Festival Read(int id)
        {
            var festival = this.repo.Read(id);
            if (festival == null)
            {
                throw new ArgumentException("Festival does not exist");
            }
            return festival;
        }

        public IQueryable<Festival> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Festival item)
        {
            this.repo.Update(item);
        }

        //Festival with the most artists
        public string FestivalWithMostArtists()
        {
            return this.repo.ReadAll()
                            .OrderByDescending(festival => festival.Artists.Count)
                            .FirstOrDefault().Name;
                            
        }

        
    }
}
