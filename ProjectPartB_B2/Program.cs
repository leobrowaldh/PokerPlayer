using System;
using System.Text;

namespace ProjectPartB_B2
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Creating a deck
            Console.OutputEncoding = Encoding.UTF8;
            DeckOfCards myDeck = new DeckOfCards();
            myDeck.CreateFreshDeck();
            Console.WriteLine($"\nA freshly created deck with {myDeck.Count} cards:");
            Console.WriteLine(myDeck);

            Console.WriteLine($"\nA sorted deck with {myDeck.Count} cards:");
            myDeck.Sort();
            Console.WriteLine(myDeck);

            Console.WriteLine($"\nA shuffled deck with {myDeck.Count} cards:");
            myDeck.Shuffle();
            Console.WriteLine(myDeck);
            #endregion

            #region PokerHands
            
            //1. From a sorted deck
            Console.WriteLine("------ Poker hands from a fresh deck -----");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            myDeck.CreateFreshDeck();
            Console.WriteLine(myDeck);
            myDeck.Reverse(); //Just to start from the bottom and get royal flush.
            PokerHand player = new PokerHand();
            
            while (myDeck.Count > 5)
            {
                Deal(myDeck, player);
                player.DetermineRank();
                PrintHandInfo(myDeck, player);
            }

            //2. From a shuffled deck
            Console.WriteLine("------ Poker hands from a shuffeled deck -----");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            myDeck.CreateFreshDeck();
            myDeck.Shuffle();
            Console.WriteLine(myDeck);
            player = new PokerHand();

            while (myDeck.Count > 5)
            {
                Deal(myDeck, player);
                player.DetermineRank();
                PrintHandInfo(myDeck, player);
            }
            #endregion

        }

        private static void PrintHandInfo(DeckOfCards myDeck, PokerHand player)
        {
            Console.WriteLine(player);
            Console.WriteLine($"Rank is {player.Rank} with rank-high-card {player.RankHiCard}");
            if (player.Rank == PokerRank.TwoPair)
            {
                Console.WriteLine($"The highest pair is of rank {player.RankHiCardPair1}");
                Console.WriteLine($"The lowest pair is of rank {player.RankHiCardPair2}");
            }
            Console.WriteLine($"Deck now has {myDeck.Count} cards.\n");
        }

        private static void Deal(DeckOfCards myDeck, PokerHand player)
        {
            player.Clear();
            while (player.Count < 5)
            {
                PlayingCard drawnCard = myDeck.RemoveTopCard();
                player.Add(drawnCard);
            }
            player.Sort();
        }
    }
 }
