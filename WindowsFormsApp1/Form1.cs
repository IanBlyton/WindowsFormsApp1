using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    //Simple app to work out change from an item cost and money tendered.
    //Assume using GBP.
    //Assume do basic validation.
    //Assume do not include tests.
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Calculate_Button_Click(object sender, EventArgs e)
        {
            string message = "";
            CurrencyCalculator calc = new CurrencyCalculator();
            calc.MoneyIn = txtMoneyIn.Text;
            
            if (!calc.ValidateMoneyIn())
            {
                message = "Money In is invalid. Please correct." + Environment.NewLine;
            }
            calc.ItemCost = txtItemCost.Text;
            if (!calc.ValidateItemCost())
            {
                message += "Item Cost is invalid. Please correct.";
            }
            if (message != "")
            {
                txtMessage.Text = message;
                return;
            }
            txtMessage.Text = calc.Calculate();
        }
    }
}
