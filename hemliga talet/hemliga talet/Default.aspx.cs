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
        private int? PrevGuess
        {
            get { return Session["PrevGuess"] as int?; }
            set { Session["PrevGuess"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var input = int.Parse(Input.Text);
            string message = "";

            if (!PrevGuess.HasValue)
            {
                obj = new SecretNumber();
            }
            obj.MakeGuess(input);
            obj.Count++;

            PrevGuessLabel.Text = string.Join(",", obj.PreviousGuesses.ToArray());
            ResultPlaceHolder.Visible = true;

            switch(){
                case Outcome.Low:
                    message = "Gissningen var för låg!";
                    break;
                case Outcome.High:
                    message = "Gissningen var för Hög!";
                    break;
                case Outcome.Correct:
                    message = String.Format("Korrekt gissat!! Det hemliga talet var {0}!", input);
                    break;
                case Outcome.PreviousGuess:
                    message = "Du har redan gissat på det talet!";
                    break;
                case Outcome.NoMoreGuesses:
                    message = String.Format("Du har inga gissningar kvar! Det hemliga talet var {0}!", obj.Number);
                    break;
            }
            HelpTextLable.Text = message;
            Session["prevGuess"] = obj;


                  


        }
    }
}