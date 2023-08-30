/*
 Mohamed Elarbi
Cis-130
Assignment #2 
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment2_CoffeeShop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool isValidentry( TextBox textBox,string name)
        {
            if (textBox.Text == "")
            {
                MessageBox.Show(name + " Is a required please try again.", "Entry Error");
                textBox.Focus();
                return false;
            }
            
            return true;
        }
        public bool isInt(TextBox textBox, string name,CheckBox checkBox)
        {
            int num =0;
            if(int.TryParse(textBox.Text,out num)==true)
            {
                return true;
            }
            else if(int.TryParse(textBox.Text, out num) == false &&checkBox.Checked==true)
            {
                MessageBox.Show(name + " Must be a Integer value", "Entry Error");
                return false; 
            }
            return true;
        }
        public bool isPositive(TextBox textBox, string name)
        {
            int num = 0;
            if (int.TryParse(textBox.Text, out num) == true)
            {
                if (num<1)
                {
                    MessageBox.Show(name + " must be at least one and cannot be a negative number", "Entry Error");
                    return false;
                }
            
            }
            return true;
        }
        public bool radioisValid(RadioButton radioButton,RadioButton radioButton2,RadioButton radioButton3, RadioButton radioButton4)
        {
            if (radioButton.Checked == false && radioButton2.Checked == false)
            {
                MessageBox.Show( " This is a required please try again.", "Selection Error");
                radioButton.Focus();
                return false;
            }
            if (radioButton3.Checked == false&&radioButton4.Checked==false)
            {
                MessageBox.Show(" This is a required please try again.", "Entry Error");
                radioButton4.Focus();
                return false;
            }
            return true;
        }
        public bool isChecked(CheckBox checkBox, CheckBox checkBox2, CheckBox checkBox3, CheckBox checkBox4, CheckBox checkBox5)
        {
            if(checkBox.Checked==false&& checkBox2.Checked== false&& checkBox3.Checked == false&&checkBox4.Checked==false&& checkBox5.Checked == false)
            {
                MessageBox.Show(" Checkbox is is a required field; at least one must be selected please try again.", "Selection Error");
                checkBox.Focus();
                return false;
            }
            return true;
        }
        public bool lstisChecked(ListBox listBox,string name)
        {
            if (listBox.SelectedIndex == -1)
            {
                MessageBox.Show(name + " is a required field", "Selection Error");
                return false;
            }
            return true;
        }
        public bool validData()
        {
            return isValidentry(txtCustomerName, "Customer Name") &&
            isValidentry(txtEmail, "Email Address") &&
            isValidentry(txtPhone, "Phone Number") &&
            radioisValid(rdoRes, rdoNRes, rdoY, rdoN) &&
            isInt(txtQuantity1, "Quantity", chkChoclate) &&
            isInt(txtQuantity2, "Quantity", chkCoffee) &&
            isInt(txtQuantity3, "Quantity", chkLatte) &&
            isInt(txtQuantity4, "Quantity", chkEspresso) &&
            isInt(txtQuantity5, "Quantity", chkTea) &&
            isPositive(txtQuantity1, "Quantity") &&
            isPositive(txtQuantity2, "Quantity") &&
            isPositive(txtQuantity3, "Quantity") &&
            isPositive(txtQuantity4, "Quantity") &&
            isPositive(txtQuantity5, "Quantity") &&
            isChecked(chkChoclate, chkCoffee, chkLatte, chkEspresso, chkTea) &&
            lstisChecked(lstVisits,"Number of visits");


        }
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (validData() == false) return; 
            const double HOT_CHOCOLATE = 2.10;
            const double BREWED_COFFEE = 2.25;
            const double HOT_LATTE = 4.10;
            const double ESPRESSO = 1.95;
            const double HOT_TEA = 2.65;
            const double TAX = 0.06;
            int intquantity1=0;
            int intquantity2=0;
            int intquantity3=0;
            int intquantity4=0;
            int intquantity5=0;
            double dblamount=1;
            discount(lstVisits.SelectedIndex, ref dblamount);
            String strAmount="";
            if (dblamount == 0.9)
            {
                strAmount = "10%";
            }
            else if (dblamount == 0.8)
            {
                strAmount = "20%";
            }
            else if (dblamount == 0.7)
            {
                strAmount = "30%";
            }
            int.TryParse(txtQuantity1.Text, out intquantity1);
            int.TryParse(txtQuantity2.Text, out intquantity2);
            int.TryParse(txtQuantity3.Text, out intquantity3);
            int.TryParse(txtQuantity4.Text, out intquantity4);
            int.TryParse(txtQuantity5.Text, out intquantity5);
            double hotChocolate_Total= HOT_CHOCOLATE* intquantity1;
            double brewedCoffee_Total = BREWED_COFFEE * intquantity2;
            double hotLatte_Total = HOT_LATTE * intquantity3;
            double espresso_Total = ESPRESSO * intquantity4;
            double hotTea_Total= HOT_TEA* intquantity5;
            double sTotal = (hotChocolate_Total + brewedCoffee_Total + hotLatte_Total + espresso_Total + hotTea_Total)*(dblamount);
            double dblTaxAmount = (hotChocolate_Total + brewedCoffee_Total + hotLatte_Total + espresso_Total + hotTea_Total) * (dblamount)*(TAX);
            double dbldiscount = (hotChocolate_Total + brewedCoffee_Total + hotLatte_Total + espresso_Total + hotTea_Total) - (sTotal);
           // Tried to use a boolean and if statement to get this part to work (:
            if (rdoY.Checked == true)
            {
                const double DBLCHANGE = 0.04;
                double dblDGTotal = sTotal + dblTaxAmount+DBLCHANGE;
                lblChangel.Visible = true; //Only is displayed if the donation is selected.
                lblChange.Visible = true;
                lblSTotal.Text = "$" + sTotal.ToString("0.00");
                lblDiscount.Text = strAmount + "  -$" + dbldiscount.ToString("0.00");
                lblTax.Text = "$" + dblTaxAmount.ToString("0.00");
                lblGrandTotal.Text = "$" + dblDGTotal.ToString("0.00");
                lblChange.Text = "4 Cents";
            }
            else if (rdoN.Checked = true)
            {
                lblChangel.Visible = false;
                lblChange.Visible = false;
                double dblGTotal = sTotal + dblTaxAmount;
                lblSTotal.Text = "$" + sTotal.ToString("0.00");
                lblDiscount.Text = strAmount + "  -$" + dbldiscount.ToString("0.00");
                lblTax.Text = "$" + dblTaxAmount.ToString("0.00");
                lblGrandTotal.Text = "$" + dblGTotal.ToString("0.00");
            }
      
            
          
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCustomerName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtQuantity1.Clear();
            txtQuantity2.Clear();
            txtQuantity3.Clear();
            txtQuantity4.Clear();
            txtQuantity5.Clear();
            rdoRes.Checked = false;
            rdoNRes.Checked = false;
            rdoY.Checked = false;
            rdoN.Checked = false;
            chkChoclate.Checked = false;
            chkCoffee.Checked = false;
            chkEspresso.Checked = false;
            chkLatte.Checked = false;
            chkTea.Checked = false;
            lblChange.Text = string.Empty;
            lblChange.Visible = false;
            lblChangel.Visible = false;
            lblGrandTotal.Text = string.Empty;
            lblSTotal.Text = string.Empty;
            lblDiscount.Text = string.Empty;
            lblTax.Text = string.Empty;
            lblGrandTotal.Text = string.Empty;
            lstVisits.SelectedIndex = -1;
            txtCustomerName.Focus();
        }

        private void chkChoclate_CheckedChanged(object sender, EventArgs e)
        {
           
            if (chkChoclate.Checked == true)
            {
                txtQuantity1.Visible = true;
                lblQuantity1.Visible = true;
            }
            if (chkChoclate.Checked == false)
            {
                txtQuantity1.Visible = false;
                lblQuantity1.Visible = false;
            }
        }

        private void chkCoffee_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCoffee.Checked == true)
            {
                txtQuantity2.Visible = true;
                lblQuantity2.Visible = true;
            }
            if (chkCoffee.Checked == false)
            {
                txtQuantity2.Visible = false;
                lblQuantity2.Visible = false;
            }
        }

        private void chkLatte_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLatte.Checked == true)
            {
                txtQuantity3.Visible = true;
                lblQuantity3.Visible = true;
            }
            if (chkLatte.Checked == false)
            {
                txtQuantity3.Visible = false;
                lblQuantity3.Visible = false;
            }
        }
        /*
             These are  additional features to the program, I felt like it needed to be added.
             */
        private void chkEspresso_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEspresso.Checked == true)
            {
                txtQuantity4.Visible = true;
                lblQuantity4.Visible = true;
            }
            if (chkEspresso.Checked == false)
            {
                txtQuantity4.Visible = false;
                lblQuantity4.Visible = false;
            }
        }

        private void chkTea_CheckedChanged(object sender, EventArgs e)
        {
            
            if (chkTea.Checked == true)
            {
                txtQuantity5.Visible = true;
                lblQuantity5.Visible = true;
            }
            if (chkTea.Checked == false)
            {
                txtQuantity5.Visible = false;
                lblQuantity5.Visible = false;
            }
        }
        public void discount(int intvisits, ref double dblamount)
        {
            switch (intvisits)
            {
                case 5:
                    dblamount = 0.9;
                    break;
                case 6:
                    dblamount = 0.9;
                    break;
                case 7:
                    dblamount = 0.9;
                    break;

                case 8:
                    dblamount = 0.9;
                    break;

                case 9:
                    dblamount = 0.9;
                    break;
                case 10:
                    dblamount = 0.9;
                    break;
                case 11:
                    dblamount = 0.8;
                    break;

                case 12:
                    dblamount = 0.8;
                    break;

                case 13:
                    dblamount = 0.8;
                    break;

                case 14:
                    dblamount = 0.8;
                    break;

                case 15:
                    dblamount = 0.8;
                    break;

                case 16:
                    dblamount = 0.8;
                    break;
                case 17:
                    dblamount = 0.8;
                    break;
                case 18:
                    dblamount = 0.8;
                    break;
                case 19:
                    dblamount = 0.8;
                    break;

                case 20:
                    dblamount = 0.8;
                    break;
                case 21:
                    dblamount = 0.7;
                    break;

                case 22:
                    dblamount = 0.7;
                    break;

                case 23:
                    dblamount = 0.7;
                    break;

                case 24:
                    dblamount = 0.7;
                    break;

                case 25:
                    dblamount = 0.7;
                    break;

                case 26:
                    dblamount = 0.7;
                    break;

                case 27:
                    dblamount = 0.7;
                    break;

                case 28:
                    dblamount = 0.7;
                    break;

                case 29:
                    dblamount = 0.7;
                    break;
                case 30:
                    dblamount = 0.7;
                    break;
                case 31:
                    dblamount = 0.7;
                    break;
            }
        }
    }
}
