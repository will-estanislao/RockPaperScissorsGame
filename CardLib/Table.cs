using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLib
{
    public class Table
    {
        // Table will send info to player/GUI/GameLogic & stats

        // For now just have cards showing on table
        // Flip the cards
        // decide winner?

        // Could have table absorbed into game logic?

        #region FIELDS & PROPERTIES
        private List<Cards> cardsOnTable;
        private List<Cards> currentDeck;
        private List<Cards> discardPile;
        private int currentPlayer; // takes player number
        private readonly string playerName;
        #endregion

        #region CONSTRUCTORS
        // On initialization of table allow empty array
        private Table()
        {
            // Create new empty list for the cards on the table
            cardsOnTable = new List<Cards>();
        }

        public Table(Deck deckInUse)
        {
            // Empty cards on table
            // Allow deck to be stored here too?
            // make deck methods accessible to table and only table can manip deck?

            // Create new empty list for the cards on the table
            cardsOnTable = new List<Cards>();
            discardPile = new List<Cards>(deckInUse.Decksize);
            currentDeck = new List<Cards>(deckInUse.Decksize);
            currentDeck.AddRange(deckInUse.Cards);
        }
        #endregion

        #region METHODS
        /// <summary>
        /// View the cards currently stored in the table
        /// </summary>
        public void ViewCards()
        {
            // Check if there are cards on the table right now
            if (cardsOnTable == null || cardsOnTable.Count == 0)
            {
                Console.WriteLine("There's no cards on the table!\n");
            }
            else
            {
                Console.WriteLine("The current cards on the table:\n");
                foreach (Cards card in cardsOnTable)
                {
                    if (!card.IsFaceUp)
                    {
                        Console.WriteLine("Card Hidden\n"); // If the card is face down, don't show
                    }
                    else
                    {
                        Console.WriteLine(card);
                    }

                }
            }
            
        }

        /// <summary>
        /// Take cards picked by the player to place on the table
        /// The cards chosen by the player will be stored in the 
        /// table's card list which can be viewed/used to determine winner
        /// 
        /// </summary>
        /// <param name="currentPlayer"></param>
        /// <param name="chosenCard"></param>
        public void PlaceCardsOnTable(Player currentPlayer, int chosenCard)
        {
            cardsOnTable.Add(currentPlayer.PlaceCard(chosenCard)); // Add cards chosen by the player to table

            // Print out results of what is on table and also show player hand after
            /*Console.WriteLine("Player: {0}\nCards in Hand: ", currentPlayer.PlayerName);
            foreach (Cards card in currentPlayer.Hand)
            {
                Console.WriteLine("{0}", card);
            }*/
            
        }

        // Determines winner of round - table may not necessarily do this, can be something else
        //                              but i'm putting it in table bc it has all the info it needs to figure this out
        //                              Need to find a way to figure out who goes first/turn order etc. 
        public void DetermineWinner(Player p1, Player p2, bool playerFirst)
        {
            // Ensure at least 2 cards played
            if (cardsOnTable.Count >= 2)
            {
                List<Cards> tempCardHold = new List<Cards>(cardsOnTable);

                // Go thru most recent cards on table and flip them - Game Specific rule!!
                for (int i = tempCardHold.Count - 2; i < cardsOnTable.Count; i++)
                {
                    tempCardHold[i].FlipCard();
                }

                // Update Cards on table to be flipped
                UpdateCardsOnTable(tempCardHold);

                // Retrieve beginning of list
                int beginningList = tempCardHold.IndexOf(tempCardHold[0]);

                // Move the most recent cards on table to index 0 and 1
                tempCardHold.RemoveRange(beginningList, tempCardHold.Count - 2);

                // If player 1 doesn't go first reverse the card order
                if (!playerFirst)
                {
                    tempCardHold.Reverse();
                }

                // Determine whose card wins the round by checking the sign of the card(Rock/Paper/Scissor)
                // Declare winner - Console output needs to change depending on # of players
                //                  right now only 2 players can play and winner names are hard coded
                //                  This also assumes that player 1 is always to the left while player 2 is to the right
                //                  Player1 must always go first
                if (tempCardHold[0] == tempCardHold[1])
                {
                    Console.WriteLine("It's a tie...\n");
                }
                else
                {
                    if (tempCardHold[0] > tempCardHold[1])
                    {
                        Console.WriteLine("Player 1 wins with {0}\n", tempCardHold[0].ToString());
                        p2.NumberOfLives--;
                    }
                    else if (tempCardHold[0] < tempCardHold[1])
                    {
                        Console.WriteLine("Player 2 wins with {0}\n", tempCardHold[1].ToString());
                        p1.NumberOfLives--;
                    }
                }

                ViewCards();  // View Cards at the table

                discardPile.AddRange(tempCardHold); // Put cards aside in discard pile
                
            }
            else
            {
                throw new ArgumentException("There aren't enough cards on the table");
            }
        }
        
        public void UpdateCardsOnTable(List<Cards> toAdd)
        {
            cardsOnTable.Clear();
            cardsOnTable.AddRange(toAdd);
        }
        
        // Update stats?
        #endregion

        public override string ToString()
        {
            string tableContents;
            return string.Format("The table contains: \n{0}\n", cardsOnTable);
        }
    }
}
