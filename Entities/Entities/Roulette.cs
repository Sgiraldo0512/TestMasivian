using System;
using System.Collections.Generic;

namespace Masivian.Roulette.Entities
{

    [Serializable]
    public partial class Roulette
    {
        public string Id { get; set; }

        public bool Open { get; set; }

        public IDictionary<string, double>[] Panel { get; set; } = new IDictionary<string, double>[39];


        public Roulette()
        {
            for (int i = 0; i < Panel.Length; i++)
            {
                Panel[i] = new Dictionary<string, double>();

            }
        }

        public void SetColors()
        {
            Panel[Panel.Length - 1] = new Dictionary<string, double>{
                                     { "Black", 0}
                                        };

            Panel[Panel.Length - 2] = new Dictionary<string, double>{
                                     { "Red", 0}
                                        };
        }
    }
}
