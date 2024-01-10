using Helpers;
using System;
using System.Text;

namespace ProjectPartB_B1
{
    class Program
    {
        static void Main(string[] args)
        {
            # region: Creating a Deck

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

            #region: 2 Player game

            myDeck.Shuffle();
            HandOfCards player1 = new HandOfCards();
            HandOfCards player2 = new HandOfCards();

            bool correctInput1 = csConsoleInput.TryReadInt32("How many cards should be dealt to each player?", 1, 5, out int numberOfCards);
            bool correctInput2 = false;
            int numberOfRounds = 0;
            if ( correctInput1 )
            {
                correctInput2 = csConsoleInput.TryReadInt32("How many rounds do you want to play?", 1, 5, out numberOfRounds);
            }
            //if input is ok, we run the game:
            if (correctInput1 && correctInput2 )
            {
                Game(myDeck, player1, player2, numberOfCards, numberOfRounds);
            }

            #endregion

        }
        /// <summary>
        /// Runs the game sequence, dealing cards every rounds, determining the winner of the round,
        /// and finally determining the Winner of the game.
        /// </summary>
        /// <param name="myDeck"></param>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <param name="numberOfCards"></param>
        /// <param name="numberOfRounds"></param>
        private static void Game(DeckOfCards myDeck, HandOfCards player1, HandOfCards player2, int numberOfCards, int numberOfRounds)
        {
            Console.WriteLine("\nLet the game begin!\n");
            Console.WriteLine("PRESS ANY KEY");
            Console.ReadKey(true);

            for (int i = 1; i <= numberOfRounds; i++)
            {
                Deal(myDeck, numberOfCards, player1, player2);
                PrintRound(myDeck, player1, player2, numberOfCards, i);
                DetermineWinner(player1, player2);

                Console.WriteLine("---------------------------------");
                Console.WriteLine($"Score: player1: {player1?.Wins ?? 0} | player2: {player2?.Wins ?? 0}");
                Console.WriteLine("---------------------------------");
                Console.WriteLine("PRESS ANY KEY");
                Console.ReadKey(true);
            }

            EndGame(player1, player2);
        }
        
        /// <summary>
        /// Determines and prints who won the game
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        private static void EndGame(HandOfCards player1, HandOfCards player2)
        {
            Console.Clear();
            Console.WriteLine("FINAL RESULT:");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine($"Score: player1: {player1?.Wins ?? 0} | player2: {player2?.Wins ?? 0}");
            Console.WriteLine("-------------------------------------------");
            if (player1.Wins > player2.Wins)
            {
                Console.WriteLine("Player 1 Won the game!");
            }
            else if (player1.Wins < player2.Wins)
            {
                Console.WriteLine("Player 2 Won the game!");
            }
            else
            {
                Console.WriteLine("It's a tie.");
            }
        }

        /// <summary>
        /// Print every players cards in this round as well as their lowest and highest card.
        /// </summary>
        /// <param name="myDeck"></param>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <param name="numberOfCards"></param>
        /// <param name="round"></param>
        private static void PrintRound(DeckOfCards myDeck, HandOfCards player1, HandOfCards player2, int numberOfCards, int round)
        {
            Console.Clear();
            Console.WriteLine("-----------------");
            Console.WriteLine($"Round Nr{round}");
            Console.WriteLine("-----------------");
            Console.WriteLine($"{numberOfCards} cards dealt to each player. {myDeck.Count} cards remaining in deck.\n");
            Console.WriteLine($"player1 hand with {numberOfCards} cards.");
            Console.WriteLine($"Lowest card in hand is {player1.Lowest} and highest is {player1.Highest}:");
            Console.WriteLine(player1);
            Console.WriteLine();
            Console.WriteLine($"player2 hand with {numberOfCards} cards.");
            Console.WriteLine($"Lowest card in hand is {player2.Lowest} and highest is {player2.Highest}:");
            Console.WriteLine(player2);
            Console.WriteLine();
        }


        /// <summary>
        /// Removes from myDeck one card at the time and gives it to player1 and player2. 
        /// Repeated until players have recieved nrCardsToPlayer 
        /// </summary>
        /// <param name="myDeck">Deck to remove cards from</param>
        /// <param name="nrCardsToPlayer">Number of cards each player should recieve</param>
        /// <param name="player1">Player 1</param>
        /// <param name="player2">Player 2</param>
        private static void Deal(DeckOfCards myDeck, int nrCardsToPlayer, HandOfCards player1, HandOfCards player2)
        {
            player1.Clear();
            player2.Clear();
            while (player1.Count < nrCardsToPlayer && player2.Count < nrCardsToPlayer)
            {
                PlayingCard drawnCard = myDeck.RemoveTopCard();
                player1.Add(drawnCard);
                drawnCard = myDeck.RemoveTopCard();
                player2.Add(drawnCard);
            }                                                      
        }

        /// <summary>
        /// Determines the winning player and add to it's score, print out the winner.
        /// Player with higest card wins. If both cards have equal value it is a tie.
        /// </summary>
        /// <param name="player1">Player 1</param>
        /// <param name="player2">Player 2</param>
        /// <param name="winner">winner</param>
        private static void DetermineWinner(HandOfCards player1, HandOfCards player2)
        {   //we compare only value here, since color makes no difference
            if (player1.Highest.Value > player2.Highest.Value) 
            {
                player1.Wins += 1;
                Console.WriteLine("player1 win!");
            } 
            else if (player1.Highest.Value < player2.Highest.Value) 
            { 
                player2.Wins += 1;
                Console.WriteLine("player2 win!");
            }
            else Console.WriteLine("it's a tie!");
        }
    }
}
