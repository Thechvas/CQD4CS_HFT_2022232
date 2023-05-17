using CQD4CS_HFT_2022232.Models;
using CQD4CS_HFT_2022232.Repository.Database;
using CQD4CS_HFT_2022232.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQD4CS_HFT_2022232.Repository.ModelRepositories
{
    public class SongRepository : Repository<Song>
    {
        public SongRepository(FestivalDbContext ctx) : base(ctx)
        {

        }

        public override Song Read(int id)
        {
            return ctx.Songs.FirstOrDefault(s => s.Id == id);
        }

        public override void Update(Song item)
        {
            var old = Read(item.Id);
            foreach (var prop in item.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
