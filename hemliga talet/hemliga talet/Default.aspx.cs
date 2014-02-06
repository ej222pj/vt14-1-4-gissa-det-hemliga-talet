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
            if (IsValid)
            {
                //Ger Innput focus
                Page.SetFocus(Input);

                //Deklarerar en ny secret number. (Kommer inte ihåg ordet för det)
                if (SecretNumber == null)
                {
                    SecretNumber = new SecretNumber();
                }

                int input = int.Parse(Input.Text);

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
                //Visar felmedelande
                

                //Om man inte kan gissa mer
                if (!SecretNumber.CanMakeGuess)
                {
                    SendGuessButton.Enabled = false;//Dissable gissa knapp
                    Input.Enabled = false;//Disable input
                    Page.SetFocus(NewNumberButton);//Get Nytt spelknappen focus
                    NewButtonPlaceHolder.Visible = true;//Visar nya knappen
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

            SendGuessButton.Enabled = true;//Enablar och ger focus till input och gisa knappen
            Input.Enabled = true;
            Page.SetFocus(Input);
        }//Startover knapp
    }//Class
}//Namespace