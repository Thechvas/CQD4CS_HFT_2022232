using CQD4CS_HFT_2022232.Logic.Interfaces;
using CQD4CS_HFT_2022232.Models;
using CQD4CS_HFT_2022232.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQD4CS_HFT_2022232.Logic.Classes
{
    public class ArtistLogic : IArtistLogic
    {
        IRepository<Artist> repo;
        public ArtistLogic(IRepository<Artist> repo)
        {
            this.repo = repo;
        }

        public void Create(Artist item)
        {
            if (item.NumOfAlbums < 0) 
            {
                throw new ArgumentException("Number of albums cannot be negative!");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Artist Read(int id)
        {
            var artist = this.repo.Read(id);
            if (artist == null)
            {
                throw new ArgumentException("Artist does not exist");
            }
            return artist;
        }

        public IQueryable<Artist> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Artist item)
        {
            this.repo.Update(item);
        }
    }
}
