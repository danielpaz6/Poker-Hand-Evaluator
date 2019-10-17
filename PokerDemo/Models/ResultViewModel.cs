using PokerHandEvaluator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokerDemo.Models
{
    public class ResultViewModel
    {
        public Room Room { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public List<Room.SidePot> SidePots { get; set; }
        public List<List<Room.HandRank>> TotalWinners { get; set; }

        public Tuple<Room.HandRank, int> FindUserSidePot(ApplicationUser user)
        {
            for(int i = 0; i < TotalWinners.Count(); i++)
            {
                for(int j = 0; j < TotalWinners[i].Count(); j++)
                {
                    if (TotalWinners[i][j].User == user)
                        return new Tuple<Room.HandRank, int>(TotalWinners[i][j], i+1);
                }
            }

            return null;
        }
    }
}