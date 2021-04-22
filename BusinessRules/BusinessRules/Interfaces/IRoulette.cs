using Masivian.Roulette.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masivian.Roulette.BusinessRules.Interfaces
{
    public interface IRoulette
    {
        Entities.Roulette Create();
        Entities.Roulette GetById(string id);
        bool Open(string id);
        List<Entities.Roulette> GetAll();
        bool Bet(string userId, string idRoulette, Bet bet);
        Entities.CloseRoulette Close(string id);
        bool Delete(string id);
    }
}
