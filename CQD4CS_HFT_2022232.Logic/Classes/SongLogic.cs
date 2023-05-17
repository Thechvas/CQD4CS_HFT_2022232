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
    public class SongLogic : ISongLogic
    {
        IRepository<Song> repo;
        public SongLogic(IRepository<Song> repo)
        {
            this.repo = repo;
        }

        public void Create(Song item)
        {
            if (item.Lenght < 0)
            {
                throw new ArgumentException("Lenght of songs cannot be negative!");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Song Read(int id)
        {
            var song = this.repo.Read(id);
            if (song == null)
            {
                throw new ArgumentException("Song does not exist");
            }
            return song;
        }

        public IQueryable<Song> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Song item)
        {
            this.repo.Update(item);
        }
    }
}
