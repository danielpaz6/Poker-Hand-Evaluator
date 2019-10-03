using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHandEvaluator
{
    class ApplicationUser
    {
        public string Name { get; set; }
        public int Chips { get; set; }
        public List<string[]> PlayerCards { get; set; }
        public bool IsPlayingThisRound { get; set; } = true;
        public bool IsPlayingThisGame { get; set; } = true;
    }
}
