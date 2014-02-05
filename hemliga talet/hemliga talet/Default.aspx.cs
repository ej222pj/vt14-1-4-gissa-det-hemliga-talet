using hemliga_talet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hemliga_talet
{
    public partial class Default : System.Web.UI.Page
    {
        private SecretNumber SecretNumber
        {
            get
            {
                return Session["SecretNumber"] as SecretNumber;
            }
            set
            {
                if (value is SecretNumber)
                {
                    Session["SecretNumber"] = value;
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void Guess_Click(object sender, EventArgs e)
        {
            if(IsValid)
            {
                int input = int.Parse(Input.Text);

                SecretNumber.MakeGuess(input); 

                if (SecretNumber.Outcome == Outcome.NoMoreGuesses)
                {
                    HelpTextLable.Text = String.Format("Du kan inte göra fler gissningar. Det hemliga talet var {0}.", SecretNumber.Number);
                }
                else if (SecretNumber.Outcome == Outcome.PreviousGuess)
                {
                    HelpTextLable.Text = String.Format("Du har redan gissat på {0}.", input);
                }
                else if (SecretNumber.Outcome == Outcome.Low)
                {
                    HelpTextLable.Text = "Du gissade för lågt.";
                }
                else if (SecretNumber.Outcome == Outcome.High)
                {
                    HelpTextLable.Text = "Du gissade för högt.";
                }
                else
                {
                    HelpTextLable.Text = String.Format("Grattis! Du klarade det på {0} gissningar.", SecretNumber.Count);
                }
                ResultPlaceHolder.Visible = true;

                if (SecretNumber.CanMakeGuess)
                {
                    SendGuessButton.Enabled = true;
                    Input.Enabled = true;
                    Page.SetFocus(NewNumberButton);
                    Page.SetFocus(Input);
                    NewNumberButton.Enabled = false;
                }

                // Skriver ut gissningarna
                foreach (var item in SecretNumber.PreviousGuesses)
                {
                    PrevGuessLabel.Text = string.Join(", ", SecretNumber.PreviousGuesses);
                } 
            }
        }
        protected void Startover_Click(object sender, EventArgs e) 
        {
            if (SecretNumber == null)
            {
                SecretNumber = new SecretNumber();
            }
            else
            {
                SecretNumber.Initialize();
            }

            NewNumberButton.Enabled = false;
            SendGuessButton.Enabled = true;
            Input.Enabled = true;
            Page.SetFocus(Input);
        }
    }
}