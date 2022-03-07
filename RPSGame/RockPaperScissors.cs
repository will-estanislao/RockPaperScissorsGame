using System;
using static System.Console;
using CardLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RPSGame
{
    class RockPaperScissors
    {
        static void Main(string[] args)
        {
            #region NOTES
            /*
             * A game of RPS. Shows some of the functionality of the game.
             * 
             * Right now A.I. is not a separate class, just another player instantiated
             * It won't make any smart decisions at all, all it does is respond.
             * Basically it will just pick the first card in its hand and drop it onto the
             * table (no thoughts, head absolutely empty).
             * 
             * Not much validation on user input for options, it's not so important. The
             * program will close if you don't put in the right keys/output funny stuff.
             * The main point of this is less about getting all validation but more 
             * general functionality.
             * 
             * If the window keeps closing on you after entering a command, WAIT!!
             * The Sleeps in the program closes the window if you use the keyboard.
             * Be patient and wait those 2-3 seconds out! Wait for the prompts
             */
            #endregion

            #region VARIABLES
            Random sourceGen = new Random();
            bool p1First = false;
            bool p1CurrentTurn = false;
            bool playerTurn = true;
            bool gameOver = false;
            bool gameStart = true;
            int turnCount = 0;
            int deckSize = 27; // SOME RELATED VALUES ARE HARD CODED - MAY MESS UP IF YOU CHANGE SIZE
            #endregion

            #region MAIN GAME
            try
            {
                // SETUP:
                Deck rpsDeck = new Deck(deckSize);              // Create deck
                rpsDeck.Shuffle();                              // Shuffle deck
                Player p1 = new Player("Player1", rpsDeck);     // Create the players
                Player p2 = new Player("Player2", rpsDeck);
                Table gameTable = new Table(rpsDeck);           // Create table - Last to be created?

                // MAIN GAMEPLAY LOOP
                while (!gameOver)
                {
                    GameStart(); // Titlescreen options

                    p1First = CoinFlip(); // Decide who goes first

                    WriteLine("\nGame Start!!\n\n");

                    while (gameStart)
                    {
                        turnCount++;

                        // If the player is not going first, p2 will go first
                        if (!p1First)
                        {
                            // Computer turn
                            gameTable.PlaceCardsOnTable(p2, 0);
                            WriteLine("Player 2 placed a card on the table!\n\n");

                            // The player's turn
                            PlayerTurn(playerTurn, p1, rpsDeck, gameTable);
                        }
                        else
                        {
                            // The player's turn
                            PlayerTurn(playerTurn, p1, rpsDeck, gameTable);

                            // Computer turn
                            WriteLine("\nIt's player 2's turn!\n");
                            gameTable.PlaceCardsOnTable(p2, 0);
                            WriteLine("Player 2 placed a card on the table!\n\n"); 
                        }

                        gameTable.DetermineWinner(p1, p2, p1First); // Find out winner of match
                        
                        // Show the current number of lives the player has and computer player
                        WriteLine("{0}'s current life: {1}\n", p1.PlayerName, p1.NumberOfLives);    
                        WriteLine("{0}'s current life: {1}\n", p2.PlayerName, p2.NumberOfLives);

                        playerTurn = true; // Reset player turn
                        
                        Thread.Sleep(TimeSpan.FromSeconds(3.0)); // 3secs before starting new round/ending game

                        // Check is player lives are gone to finish game
                        if (p1.NumberOfLives < 1 || p2.NumberOfLives < 1)
                        {
                            WriteLine("\nThat's the end of the game!\n");
                            WriteLine("\nGame Stats:\n# of Turns:{0}", turnCount);

                            WriteLine("\nPress enter to exit...");
                            ReadKey();

                            gameOver = true;
                            gameStart = false;  
                        }
                    }
                }
            }
            catch (ArgumentException ex) when (ex.ParamName == "userErr")
            {
                Write(ex.Message);
            }
            #endregion
            
            #region GAMEPLAY FUNCTIONS
            /// Function for starting the menu options
            static void GameStart()
            {
                WriteLine("Welcome to Rock Paper Scissors!\n Please choose a menu option below:\n" +
                        "> Start Game [1]\n> Rules [2]\n> Exit [3]\n");
                WriteLine("Type Option Here: ");
                char userOption = ReadKey().KeyChar;

                switch (userOption)
                {
                    case '1':
                        break;
                    case '2':
                        WriteLine("\nRock Paper Scissors Rules\n " +
                            "Just like real life RPS but you use cards and have a set amount of cards in your hand.\n" +
                            "Every turn you'll have to draw a card from the deck after putting a card down.\n" +
                            "Decide which card to put down by typing the number associated with it.\n Good luck!");
                        WriteLine("Return to title...");
                        ReadKey();
                        GameStart();
                        break;
                    case '3':
                        WriteLine("\nSee you!");
                        Thread.Sleep(TimeSpan.FromSeconds(2.0));
                        Environment.Exit(0); // Close program
                        break;
                       
                }
            }

            /// Function for choosing who goes first
            static bool CoinFlip()
            {
                Random sourceGen = new Random();
                int rng = 0;

                bool p1Turn = false;

                WriteLine("\nWe're gonna decide who goes first!\n");
                WriteLine("I'll flip a coin. You're heads and I'll be tails!\n");

                Thread.Sleep(TimeSpan.FromSeconds(1.0));

                // Decide who goes first
                rng = sourceGen.Next(1, 3);
                if (rng == 1)
                {
                    WriteLine("It's heads.\nLooks like you go first!\n");
                    p1Turn = true;
                }
                else
                {
                    WriteLine("It's tails! You're going second!\n");
                }

                return p1Turn;
            }

            /// Actions player can take when it's their turn
            static void PlayerTurn(bool playerTurn, Player player1, Deck deckUsed, Table table)
            {

                while (playerTurn)
                {
                    WriteLine(player1.ToString()); // Show what's in the user's hand
                    WriteLine("To choose a card to place down, select the number of the card.");
                    Write("Enter your choice: ");
                    char userOption = ReadKey().KeyChar;

                    // Check if entered key is an int
                    if (int.TryParse(userOption.ToString(), out int cardChoice))
                    {
                        if (cardChoice > 0 && cardChoice <= player1.Hand.Count)
                        {
                            WriteLine("\nYou placed {0}", player1.Hand[cardChoice - 1]); // Show the card that's been placed on the table
                            table.PlaceCardsOnTable(player1, cardChoice); // Place card onto table
                            player1.DrawFromDeck(deckUsed); // Draw from the deck

                            playerTurn = false; // Get out of player turn loop - End of turn
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException("userErr", "Hey enter a number between 1 to " + player1.Hand.Count);
                        }
                    }
                    else
                    {
                        throw new ArgumentException("userErr", "Enter a valid positive numeric value!");
                    }
                }
            }
            #endregion
        }


    }
}
