using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CardLib
{
    public class Cards
    {
        #region PROPERTIES
        public Sign sign;

        private bool isFaceUp; // true = card face/ false = card back
        public bool IsFaceUp
        {
            get { return isFaceUp; }
            set { isFaceUp = value; }
        }

        private int numberVal;
        public int NumberVal { get; set; }
        #endregion

        #region CONSTRUCTORS
        public Cards(Sign newSign, bool flip, int numberValue)
        {
            sign = newSign;
            isFaceUp = flip;
            numberVal = numberValue;
        }

        /// <summary>
        /// Use this only for testing - revert back to private
        /// </summary>
        public Cards()
        {
            
        }
        
        #endregion

        #region METHODS
        public void PrintCard()
        {
            Console.WriteLine(this.NumberVal.ToString());
            Console.WriteLine(this.IsFaceUp.ToString());
        }

        public void ChangeValue(int valueChange)
        {
            this.NumberVal = valueChange;
        }

        public bool FlipCard()
        {
            if (!isFaceUp)
            {
                IsFaceUp = true;
            }
            return isFaceUp;
        }
        #endregion

        #region OVERRIDES & OVERLOADS
        public static bool operator ==(Cards card1, Cards card2)
        {
            bool isEquivalent = false;
            if(card1.sign == card2.sign)
            {
                isEquivalent = true;
            }

            return isEquivalent; 
        }

        public static bool operator !=(Cards card1, Cards card2)
        {
            bool isEquivalent = false;
            if (!(card1.sign == card2.sign))
            {
                isEquivalent = true;
            }
            return isEquivalent;
        }
        
        public static bool operator >(Cards card1, Cards card2)
        {
            if (card1.sign == Sign.Scissors)
            {
                if (card2.sign == Sign.Rock)
                {
                    return false;
                }
                else if (card2.sign == Sign.Paper)
                {
                    return true;
                }
                else
                {
                    return card1.sign > card2.sign;
                }

            }
            else if (card1.sign == Sign.Paper)
            {
                if (card2.sign == Sign.Scissors)
                {
                    return false;
                }
                else if (card2.sign == Sign.Rock)
                {
                    return true;
                }
                else
                {
                    return card1.sign > card2.sign;
                }
            }
            else if(card1.sign == Sign.Rock)
            {
                if (card2.sign == Sign.Paper)
                {
                    return false;
                }
                else if (card2.sign == Sign.Scissors)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return card1.sign == card2.sign;
            }

        }

        public static bool operator <(Cards card1, Cards card2) => !(card1 > card2);

        public override string ToString()
        {
            return String.Format("Card: {0}\n", sign);
        }
        #endregion
    }
}
