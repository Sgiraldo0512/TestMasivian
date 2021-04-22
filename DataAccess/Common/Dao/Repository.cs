using DataAccess.Common.Interfaces;
using EasyCaching.Core;
using Masivian.Roulette.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Common.Dao
{
    public class Repository : IRepository
    {
        private IEasyCachingProviderFactory _cachingProviderFactory;

        private IEasyCachingProvider _cachingProvider;

        private const string KEY = "ROULETTEKEY";

        public Repository(IEasyCachingProviderFactory easyCachingProviderFactory)
        {
            this._cachingProviderFactory = easyCachingProviderFactory;
            this._cachingProvider = this._cachingProviderFactory.GetCachingProvider(Masivian.Roulette.Utilities.Common.Constantes.NAMEREDIS);
        }
        public Masivian.Roulette.Entities.Roulette Create(Masivian.Roulette.Entities.Roulette roulette)
        {
            this._cachingProvider.Set(KEY + roulette.Id, roulette, TimeSpan.FromDays(10));
            return roulette;
        }

        public Roulette Update(string id, Roulette roulette)
        {
            roulette.Id = id;
            return Create(roulette);
        }

        public Roulette GetById(string id)
        {
            var item = this._cachingProvider.Get<Roulette>(KEY + id);

            if (!item.HasValue)
            {
                return null;
            }
            return item.Value;
        }

        public List<Roulette> GetAll()
        {
            var rouletes = this._cachingProvider.GetByPrefix<Roulette>(KEY);
            if (rouletes.Values.Count == 0)
            {
                return new List<Roulette>();
            }
            return new List<Roulette>(rouletes.Select(x => x.Value.Value));
        }

        public bool Delete(string id)
        {
            if (this.GetById(id) == null)
            {
                return false;
            }
            else
            {
                this._cachingProvider.Remove(KEY + id);
                return true;
            }

            

        }
    }
}
