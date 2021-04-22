using System;
using System.Collections.Generic;
using System.Text;

namespace Masivian.Roulette.Entities
{
    public class CloseRoulette
    {
        public double TotalRoulette { get; set; }
        public int WinnerNumber { get; set; }
        public double WinnerValue { get; set; }
        public string WinnerUserId { get; set; }
    }
}
