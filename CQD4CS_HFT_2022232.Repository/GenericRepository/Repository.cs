﻿using CQD4CS_HFT_2022232.Repository.Database;
using CQD4CS_HFT_2022232.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQD4CS_HFT_2022232.Repository.GenericRepository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected FestivalDbContext ctx;

        protected Repository(FestivalDbContext ctx)
        {
            this.ctx = ctx;
        }

        public void Create(T item)
        {
            ctx.Set<T>().Add(item);
            ctx.SaveChanges();
        }

        public void Delete(int id)
        {
            ctx.Set<T>().Remove(Read(id));
            ctx.SaveChanges();
        }

        public IQueryable<T> ReadAll()
        {
            return ctx.Set<T>();
        }

        public abstract T Read(int id);
        
        public abstract void Update(T item);



    }
}
