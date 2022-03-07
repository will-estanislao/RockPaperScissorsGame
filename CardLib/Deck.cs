using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLib
{
    public class Deck
    {
        // Deck for restricted rock paper scissors 
        // private Cards[] cards; // Represents an empty deck - Change to list later
        private List<Cards> cards;
        public List<Cards> Cards
        {
            get 
            { 
                return cards; 
            }
            set
            {
                cards = value;
            }
        }

        // Total size of the deck
        private int decksize;
        public int Decksize
        {
            get
            {
                return decksize;
            }
            set
            {
                decksize = value;
            }
        }

        #region CONSTRUCTORS
        /// <summary>
        /// Parameterized constructor for the deck
        /// Allows user to enter their own deck size
        /// </summary>
        /// <param name="deck"></param>
        public Deck(int deck)
        {
            Decksize = deck; // Set a custom size for the deck
            int totalRanks = deck / 3; // Divide by total number of card suits
            cards = new List<Cards>(decksize); // set the size of the list that will hold cards representing deck
            
            for (int signVal = 0; signVal < 3; signVal++)   // Loop thru # of signs
            {
                for (int i = 1; i < totalRanks + 1; i++)    // Fill in number of 
                {
                    // Add new card onto the list
                    cards.Add(new Cards() { sign = (Sign)signVal, IsFaceUp = false, NumberVal = i });
                }
            }
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Gets one singular card from the deck
        /// </summary>
        /// <param name="cardNum"> Index position of card in deck </param>
        /// <returns></returns>
        public Cards GetCards(int cardNum)
        {
            if (cardNum >= 0 && cardNum <= decksize)
            {
                Cards cardDrawn = cards.ElementAt(cardNum);

                ResizeDeck(cardNum, 1);

                return cardDrawn;
            }
            else
            {
                throw (new ArgumentOutOfRangeException("cardNum", cardNum, "Value must be between 0 and 27!"));
            }

            
        }

        /// <summary>
        /// Shuffle needs improvement, will miss spots because hits limits
        /// needs to go on until all spots are filled
        /// </summary>
        public void Shuffle()
        {
            List<Cards> newDeck = new List<Cards>(decksize);
            List<Cards> tempDeck = new List<Cards>();
            Random sourceGen = new Random();
            int rng = sourceGen.Next(newDeck.Count);
            bool deckRemaining = true;
            Cards tempCard;

            newDeck.AddRange(cards);

            // Shuffle Methods - Can add additional shuffle methods and use a switch statement to choose
            // Fisher-Yates shuffle
            for (int i = 0; i < 3; i++) // Perform this shuffle 3 times
            {
                deckRemaining = true;
                while (deckRemaining) // Keep randomizing until the temporary deck is filled
                {
                    tempCard = newDeck.ElementAt(rng);      // Store the card found at a random position in newDeck
                    tempDeck.Add(tempCard);                 // Add that card to the tempDeck
                    newDeck.RemoveAt(rng);                  // Remove card at that position in newDeck to prevent picking it again
                    rng = sourceGen.Next(newDeck.Count);    // Generate next index to check

                    if (tempDeck.Count == decksize)         // Check if tempDeck is filled to break out of shuffle loop
                    {
                        newDeck.Clear();                   // Clear newDeck, make sure no cards are stored in there
                        newDeck.AddRange(tempDeck);        // Copy what's in tempDeck
                        tempDeck.Clear();                  // Clear tempDeck for the next round of shuffling
                        deckRemaining = false;             
                    }
                } 
            }
            // Update the deck with the newly shuffled newDeck
            UpdateCards(newDeck);
        }

        /// <summary>
        /// Resizes the deck when a card is taken
        /// 
        /// </summary>
        /// <param name="position"> The index position the player takes the card from </param>
        /// <param name="takenCards"> The number of cards they take </param>
        public void ResizeDeck(int position, int takenCards)
        {
            if (cards != null && cards.Count > 0)
            {
                // Takes the deck in use
                List<Cards> tempList = new List<Cards>(cards);

                // Remove cards
                tempList.RemoveRange(position, takenCards);

                UpdateCards(tempList);
            }
            
        }

        // Cut deck - Reduce the total deck size & exclude certain cards?

        /// <summary>
        /// Function to update cards in deck
        /// </summary>
        /// <param name="cards"></param>
        public void UpdateCards(List<Cards> newCards)
        {
            cards.Clear();

            cards.AddRange(newCards);
        }

        /// <summary>
        /// Returns the total length of the deck in a string format
        /// </summary>
        public void ReturnLength()
        {
            Console.WriteLine("The length of the current deck is: {0}", decksize);
        }
        #endregion

        #region OVERRIDES/OVERLOADS
        public override string ToString()
        {
            int count = 0;
            string deckContents = "";   // String variable that will hold all deck content
            foreach (Cards cards in cards)
            {
                count++;
                deckContents += count + "." + cards + "\n";
            }

            // Return cards contained in the deck
            return String.Format("{0}", deckContents);
        }
        #endregion

    }
}
