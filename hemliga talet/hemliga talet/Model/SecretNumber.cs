using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace hemliga_talet.Model
{
    public enum Outcome { Indefine, Low, High, Correct, NoMoreGuesses, PreviousGuess }
    [Serializable]
    public class SecretNumber
    {

        private int _number;
        private List<int> _previousGuesses;
        public const int MaxNumberOfGuesses = 7;

        public bool CanMakeGuess 
        {
            get
            {
                return Count < MaxNumberOfGuesses && Outcome != Outcome.Correct;
            }
        }
        public int Count
        {
            get { return _previousGuesses.Count; }
        }
        public int? Number
        {
            get
            {
                if (CanMakeGuess)
                {
                    return null;
                }
                else
                {
                    return _number;
                }
            }
        }
        public Outcome Outcome { get; private set; }
        public ReadOnlyCollection<int> PreviousGuesses
        {
            get { return _previousGuesses.AsReadOnly(); }
        }
        public SecretNumber()
        {
            //Initierar en ny lista
            _previousGuesses = new List<int>(MaxNumberOfGuesses);
            //Ropar på Initialize metoden
            Initialize();
        }
        public void Initialize()
        {
            //Nytt nummer
            Random randomNumber = new Random();
            _number = randomNumber.Next(1, 101);

            //Tar bort alla gamla gissningar
            _previousGuesses.Clear();
        }
        public Outcome MakeGuess(int guess)
        {
            if (!CanMakeGuess)
            {
                throw new Exception();
            }

            if (guess < 1 || guess > 100)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (PreviousGuesses.Contains(guess))
            {
                Outcome = Outcome.PreviousGuess;
            }
            else
            {
                _previousGuesses.Add(guess);

                if (guess < _number)
                {
                    Outcome = Outcome.Low;
                }
                else if (guess > _number)
                {
                    Outcome = Outcome.High;
                }
                else
                {
                    Outcome = Outcome.Correct;
                }
            }

            return Outcome;
        }
        
    }
}