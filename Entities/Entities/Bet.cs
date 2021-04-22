using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Masivian.Roulette.Entities
{
    public class Bet
    {
        /// <summary>
        /// 37 = Black, 38 = Red. 0-36 = positions
        /// </summary>
        [Range(Utilities.Common.Constantes.MIN_ROULETTE, Utilities.Common.Constantes.MAX_ROULETTE)]
        public int Number { get; set; }

        [Range(0.5d, maximum: 10000)]
        public double Value { get; set; }
    }
}
