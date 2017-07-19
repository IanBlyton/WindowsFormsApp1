using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    //Class to calculate the change given an amount of moneyIn and an itemCost.
    //
    class CurrencyCalculator
    {
        //A tuple array with currency denominations in pence and also the display value.
        //Can be easily changed. Cou;d come from a database or config file.
        Tuple<int, string>[] currVals =
        {
            Tuple.Create (10000, "£100"),
            Tuple.Create ( 5000, "£50"),
            Tuple.Create ( 2000, "£20"),
            Tuple.Create ( 1000, "£10"),
            Tuple.Create (  500, "£5"),
            Tuple.Create (  200, "£2"),
            Tuple.Create (  100, "£1"),
            Tuple.Create (   50, "50p"),
            Tuple.Create (   20, "20p"),
            Tuple.Create (   10, "10p"),
            Tuple.Create (    5, "5p"),
            Tuple.Create (    2, "2p"),
            Tuple.Create (    1, "1p")
        };

        //This is the amount of money tendered.
        private string moneyIn;
        public string MoneyIn
        {
            set
            {
                moneyIn = value;
            }
        }

        //This is the cost of the item.
        private string itemCost;
        public string ItemCost
        {
            set
            {
                itemCost = value;
            }
        }

        private int moneyInValidated = -1;
        private int itemCostValidated = -1;

        //Call this to validate the money in.
        public bool ValidateMoneyIn()
        {
            bool valid = ValidateCurrency(moneyIn, out moneyInValidated);
            if (valid && moneyInValidated > 0)
            {
                return true;
            }
            return false;
        }

        //Call this to validate the item cost.
        public bool ValidateItemCost()
        {
            bool valid = ValidateCurrency(itemCost, out itemCostValidated);
            if (valid && itemCostValidated > 0)
            {
                return true;
            }
            return false;
        }

        //Validate the string as curency and return the value in pence;
        private bool ValidateCurrency(string currencyString, out int penceValue)
        {
            bool valid = float.TryParse(currencyString, System.Globalization.NumberStyles.Currency, System.Globalization.CultureInfo.GetCultureInfo("en-GB"), out float value);
            if (!valid)
            {
                penceValue = -1;
                return false;
            }
            float penceFloat = value * 100;
            penceValue = (int)(penceFloat);
            return true;
        }

        //Calculates the cange and does some basic validation.
        public string Calculate()
        {
            int totalChange = moneyInValidated - itemCostValidated;
            if (totalChange == 0)
            {
                return "No change due.";
            }
            if (totalChange < 0)
            {
                return "Insufficient Money In.";
            }
            return CalculateChange(totalChange);
        }

        //Returns a string with the change required, each denomination separated by a newline.
        private string CalculateChange(int totalChange)
        {
            string change = "";
            for (int iLoop = 0; iLoop < currVals.Length; iLoop++)
            {
                if (currVals[iLoop].Item1 <= totalChange)
                {
                    int test = totalChange / currVals[iLoop].Item1;
                    change += test.ToString() + " x " + currVals[iLoop].Item2 + Environment.NewLine;
                    totalChange = totalChange - (currVals[iLoop].Item1 * test);
                }
            }
            return change;
        }

    }
}
