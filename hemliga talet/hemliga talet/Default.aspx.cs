using hemliga_talet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
                return Session["SecretNumber"] as SecretNumber ?? (SecretNumber)(Session["SecretNumber"] = new SecretNumber());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Ger Input focus
            Page.SetFocus(Input);
        }

        protected void Guess_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                int input = int.Parse(Input.Text);
                NewNumberButton.Enabled = true;//Visar knappen, ta bort om kanppen ska va borta

                //Ropar på MakeGuess i secretnumber classen.
                SecretNumber.MakeGuess(input);

                if (SecretNumber.Outcome == Outcome.NoMoreGuesses)
                {//Om man gissat 7 ggr
                    HelpTextLable.Text = String.Format("Du kan inte göra fler gissningar. Det hemliga talet var {0}.", SecretNumber.Number);
                }
                else if (SecretNumber.Outcome == Outcome.PreviousGuess)
                {//Om man gissar samma som man har
                    HelpTextLable.Text = String.Format("Du har redan gissat på {0}.", input);
                }
                else if (SecretNumber.Outcome == Outcome.Low)
                {//Gissar lågt
                    HelpTextLable.Text = "Du gissade för lågt.";
                }
                else if (SecretNumber.Outcome == Outcome.High)
                {//Gissar högt
                    HelpTextLable.Text = "Du gissade för högt.";
                }
                else
                {//Gissade rätt
                    HelpTextLable.Text = String.Format("Grattis! Du klarade det på {0} gissningar.", SecretNumber.Count);
                }
                //Om man inte kan gissa mer
                if (!SecretNumber.CanMakeGuess)
                {
                    SendGuessButton.Enabled = false;//Dissable gissa knapp
                    Input.Enabled = false;//Disable input
                    Page.SetFocus(NewNumberButton);//Get Nytt spelknappen focus
                    //NewButtonPlaceHolder.Visible = true; //Om mkanppen ska vara onsynlig
                }
            }//if

            //Om helptext inte har nått värde har man gett fel input. 
            if (HelpTextLable.Text == "")
            {
                HelpTextLable.Text = "Talet är inte mellan 1-100";
            }
            // Skriver ut gissningarna och visar dom
            ResultPlaceHolder.Visible = true;
            foreach (var item in SecretNumber.PreviousGuesses)
            {
                PrevGuessLabel.Text = string.Join(", ", SecretNumber.PreviousGuesses);
            }
        }//gissa knapp

        protected void Startover_Click(object sender, EventArgs e) 
        {   //Startar ett nytt spel
            SecretNumber.Initialize();

            SendGuessButton.Enabled = true;//Enablar och ger focus till input och gissa knappen
            Input.Enabled = true;
        }//Startover knapp
    }//Class
}//Namespace