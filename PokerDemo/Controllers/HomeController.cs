using PokerDemo.Models;
using PokerHandEvaluator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace PokerDemo.Controllers
{
    public class HomeController : Controller
    {
        public void Shuffle<T>(IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET : Load Data
        public ActionResult LoadScenario()
        {
            // Creating the cards and shuffle them

            List<string[]> FullDeck = new List<string[]>();
            List<string> Names = new List<string> {"Club", "Spade", "Diamond", "Heart"};

            for(int i = 0; i < 4; i++)
            {
                for(int j = 2; j <= 14; j++)
                {
                    FullDeck.Add(new string[] { j.ToString(), Names[i] });
                }
            }

            Shuffle(FullDeck);

            Room room = new Room
            {
                RoomName = "Test Room",
                CardsOnTable = new List<string[]>
                {
                    FullDeck[0],
                    FullDeck[1],
                    FullDeck[2],
                    FullDeck[3],
                    FullDeck[4],
                }
            };

            ApplicationUser user1 = new ApplicationUser
            {
                Name = "A",
                PlayerCards = new List<string[]>
                {
                    FullDeck[5],
                    FullDeck[6],
                },
                Chips = 0
            };

            ApplicationUser user2 = new ApplicationUser
            {
                Name = "B",
                PlayerCards = new List<string[]>
                {
                    FullDeck[7],
                    FullDeck[8],
                },
                Chips = 0
            };

            ApplicationUser user3 = new ApplicationUser
            {
                Name = "C",
                PlayerCards = new List<string[]>
                {
                    FullDeck[9],
                    FullDeck[10],
                },
                Chips = 0
            };

            ApplicationUser user4 = new ApplicationUser
            {
                Name = "D",
                PlayerCards = new List<string[]>
                {
                    FullDeck[11],
                    FullDeck[12],
                },
                Chips = 0
            };

            ApplicationUser user5 = new ApplicationUser
            {
                Name = "E",
                PlayerCards = new List<string[]>
                {
                    FullDeck[13],
                    FullDeck[14],
                },
                Chips = 0
            };

            room.Chair0 = user1;
            room.Chair1 = user2;
            room.Chair2 = user3;
            room.Chair3 = user4;
            room.Chair4 = user5;

            room.PotOfChair0 = 25;
            room.PotOfChair1 = 30;
            room.PotOfChair2 = 50;
            room.PotOfChair3 = 100;
            room.PotOfChair4 = 100;


            var result = room.SpreadMoneyToWinners();

            Dictionary<string, string> fixedValue = new Dictionary<string, string>();
            Dictionary<string, string> fixedSuit = new Dictionary<string, string>();
            Dictionary<int, string> fixedPosition = new Dictionary<int, string>();

            for(int i = 2; i <= 10; i++)
                fixedValue.Add(i.ToString(), i.ToString());

            fixedValue.Add("11", "jack");
            fixedValue.Add("12", "queen");
            fixedValue.Add("13", "king");
            fixedValue.Add("14", "ace");

            fixedSuit.Add("Spade", "spades");
            fixedSuit.Add("Heart", "hearts");
            fixedSuit.Add("Diamond", "diamonds");
            fixedSuit.Add("Club", "clubs");

            fixedPosition.Add(1, "1st");
            fixedPosition.Add(2, "2nd");
            fixedPosition.Add(3, "3rd");
            fixedPosition.Add(4, "4th");
            fixedPosition.Add(5, "5th");

            // View Bags

            ViewBag.FixedValue = fixedValue;
            ViewBag.FixedSuit = fixedSuit;
            ViewBag.FixedPosition = fixedPosition;

            return View(new ResultViewModel
            {
                Users = new List<ApplicationUser> { user1, user2, user3, user4, user5 },
                Room = room,
                SidePots = result.Item1,
                TotalWinners = result.Item2
            });
        }
    }
}