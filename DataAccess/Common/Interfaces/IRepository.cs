using Masivian.Roulette.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Common.Interfaces
{
    public interface IRepository
    {
        public Roulette Create(Roulette roulette);
        Roulette GetById(string Id);
        Roulette Update(string Id, Roulette roulette);
        List<Roulette> GetAll();
        bool Delete(string id);

    }
}
