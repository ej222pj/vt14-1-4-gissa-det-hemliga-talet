using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hemliga_talet.Model
{
    enum Outcome { Indefine, Low, High, Correct, NoMoreGuesses, PreviousGuess }
    public class SecretNumber
    {

        private int _number;
        private List<int> _previousGuesses;

        const int MaxNuumberOfGuesses = 7;

        public bool CanMakeGuess
        {
            private get; 
        }
        public int Count
        {
            private get;
        }
        public int? Number
        {
            private get;
        }
        public Outcome Outcome
        {
            private get;
            private set;
        }



    }
}