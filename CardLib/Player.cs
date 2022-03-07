using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLib
{
    public class Player
    {
        #region PROPERTIES
        // Hand
        private List<Cards> hand;
        private int numberOfLives;
        private string playerName;
        private static int playerNum;     // Make it static/read-only?
        private int playerScore;

        public int NumberOfLives
        {
            get { return numberOfLives; }
            set
            {
                numberOfLives = value;
            }
        }

        /// <summary>
        /// The player's name
        /// GET: Returns the player's name
        /// SET: Ensure name is not blank/null
        /// </summary>
        public string PlayerName
        {
            get { return playerName; }
            set
            {
                // May need to have this validate elsewhere?
                if (!string.IsNullOrEmpty(value))
                {
                    playerName = value;
                }
                else
                {
                    throw new ArgumentException("Please enter a name for the player!");
                }
            }
        }

        /// <summary>
        /// GET: Returns the players current hand
        /// SET: When adding a new card to player hand check if there is room/sort cards
        /// </summary>
        public List<Cards> Hand
        {
            get { return hand; }
            set 
            {
                // Check to ensure you're never adding nothing to the hand
                if (value != null)
                {
                    hand = value;
                }
                else
                {
                    throw new ArgumentNullException("You are adding nothing to the hand!");
                }
            }
        }
        #endregion

        #region CONSTRUCTORS
        private Player ()
        {
            // Hand will always be set so player will never have a blank hand
        }

        // Public Player() constructor on initialization give an array of cards
        public Player(string name, Deck deck)
        {
            // Can put set amount of cards in players hand upon initialization
            // Put 9 cards into players hand
            hand = QuickDraw(deck);

            playerName = name;
            numberOfLives = 3;
            
        }
        #endregion

        #region METHODS
        // Draw 1 Card from the deck
        public void DrawFromDeck(Deck deck)
        {
            // Pass the deck as a parameter
            // Take deck currently in play and draw from the top
            // Add card drawn to hand/list array

            hand.Add(deck.GetCards(0));

            // deck.ResizeDeck(0, 1);

            // Prints out the card the player got from the deck - Shows last card in their hand
            // Console.WriteLine("The card I picked is {0}", hand.Last());
        }

        /// <summary>
        /// Used at the start of the game for setting up
        /// the first cards needed in the player's hand for
        /// the initial start of the game
        /// </summary>
        /// <param name="deck"></param>
        public List<Cards> QuickDraw(Deck deck)
        {
            const int NUMBER_OF_SUITS = 3;
            int INDIVIDUAL_SUIT = (deck.Decksize / NUMBER_OF_SUITS); // How many cards goes into player hand
            List<Cards> tempHand = new List<Cards>(INDIVIDUAL_SUIT);
            // Take current deck in play as parameter
            // Draw 9 cards from the deck
            // Put into a players empty hand
            for (int i = 0; i < INDIVIDUAL_SUIT; i++)
            {
                tempHand.Add(deck.GetCards(0));
            }
            
            // Subtract cards taken from total deck - not used here because deck is resized w/in GetCards()
            // deck.ResizeDeck(0, INDIVIDUAL_SUIT);

            return tempHand;
            // Console.WriteLine("These are the cards I got: {0}", tempHand.ToString());
            // Possibly add as interface - for benefit of GUI?
        }

        /// <summary>
        /// Sorts the player's cards currently held in their hand
        /// </summary>
        public void SortCards()
        {

            // Doesn't need parameters just sort whats currently in player hand
            // Switch statement for different types of sorting arrangements - need a var to choose
            // For now... just take 1st card and group same

            // Ensure hand has cards before calling SortCards()!!!

            Cards anchorCard = this.hand[0];
            Cards tempCardHold;

            // Sort according to the first card in array based on sign
            
            
        }

        /// <summary>
        /// Pick a card from the player's hand at any index and return it
        /// 
        /// </summary>
        /// <param name="pickedCard"></param>
        /// <returns></returns>
        public Cards PlaceCard(int pickedCard)
        {
            // Subtract 1 from picked card to access correct index
            if (pickedCard > 0 )
            {
                --pickedCard;
            }

            // Big problem: How to make sure card belongs to player who put it down
            //              for score keeping etc. - Decided by outside forces 
            // For now... just uhhh ignore it lol
            Cards chosenCard;

            // Takes player card picked
            // and the game table, stores/adds it onto the game table's storage?/list
            chosenCard = hand.ElementAt<Cards>(pickedCard);

            // Resize player hand
            hand.RemoveRange(pickedCard, 1);

            return chosenCard;

            // Should validation of player move happen here? 
            // Not important for this game bc any card placed is valid

        }
        
        /// <summary>
        /// Outputs the status of the player's hand
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string currentHand = "";
            for (int i = 0; i < hand.Count; i++)
            {
                currentHand += (i + 1) + ". " + hand[i] + "\n";
            }

            return String.Format("{0} hand contains: \n{1}", playerName, currentHand);
        }

        #endregion
    }
}
