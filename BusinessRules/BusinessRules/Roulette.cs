using DataAccess.Common.Interfaces;
using Masivian.Roulette.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Masivian.Roulette.BusinessRules
{
    public partial class Roulette : Interfaces.IRoulette
    {

        private IRepository _repository;

        public Roulette(IRepository repository)
        {
            this._repository = repository;
        }

        public Entities.Roulette Create()
        {
            Entities.Roulette roulette = new Entities.Roulette
            {
                Id = Guid.NewGuid().ToString(),
                Open = false
            };

            return this._repository.Create(roulette);
        }

        public Entities.Roulette GetById(string id)
        {
            return this._repository.GetById(id);
        }

        public bool Open(string id)
        {
            Entities.Roulette roulette = this._repository.GetById(id);
            if (roulette != null)
            {
                roulette.Open = true;
                this._repository.Update(id, roulette);

                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Entities.Roulette> GetAll()
        {
            return this._repository.GetAll();
        }

        public bool Bet(string userId, string idRoulette, Bet bet)
        {
            Entities.Roulette roulette = this._repository.GetById(idRoulette);
            if (roulette != null && roulette.Open)
            {
                double valueNew = 0d;
                roulette.Panel[bet.Number].TryGetValue(userId, out valueNew);
                roulette.Panel[bet.Number].Remove(userId + "");
                roulette.Panel[bet.Number].TryAdd(userId + "", bet.Value + valueNew);
                this._repository.Update(idRoulette, roulette);

                return true;
            }
            else
            {
                return false;
            }
        }

        public Entities.CloseRoulette Close(string id)
        {
            Entities.Roulette roulette = this._repository.GetById(id);
            Entities.CloseRoulette closeRoulette = new CloseRoulette();
            if (roulette != null && roulette.Open)
            {
                string userId = String.Empty;
                roulette.Open = false;
                int winnerNumber = new Random().Next(Utilities.Common.Constantes.MIN_ROULETTE, Utilities.Common.Constantes.MAX_ROULETTE);
                userId = roulette.Panel[winnerNumber].Keys.FirstOrDefault();
                double winnerValue = CalculateWinnerValue(userId, winnerNumber, roulette);
                double sumAllRoulette = CalculateTotalValueRoulette(roulette);
                this._repository.Update(roulette.Id, roulette);
                closeRoulette = new CloseRoulette
                {
                    TotalRoulette = sumAllRoulette,
                    WinnerNumber = winnerNumber,
                    WinnerValue = winnerValue,
                    WinnerUserId = userId
                };
            }

            return closeRoulette;
        }

        public double CalculateWinnerValue(string userId, int winnerNumber, Entities.Roulette roulette)
        {
            double winnerValue = 0d;
            double valueBet = roulette.Panel[winnerNumber].Where(x => x.Key.ToString() == userId).Select(s => s.Value).FirstOrDefault();
            if (winnerNumber >= 0 && winnerNumber <= 36)
            {
                winnerValue = valueBet <= 0 ? 0 : valueBet * 5;
            }
            else
            {
                winnerValue = valueBet <= 0 ? 0 : valueBet * 1.8;
            }

            return winnerValue;
        }

        public double CalculateTotalValueRoulette(Entities.Roulette roulette)
        {
            List<double> values = roulette.Panel.SelectMany(x => x.Values).ToList();
            double sumAllRoulette = 0d;
            foreach (var item in values)
            {
                sumAllRoulette += item;
            }

            return sumAllRoulette;
        }
        public bool Delete(string id)
        {
            return this._repository.Delete(id);
        }
    }



}
