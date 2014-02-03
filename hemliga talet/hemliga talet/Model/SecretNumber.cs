using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace hemliga_talet.Model
{
    public enum Outcome { Indefine, Low, High, Correct, NoMoreGuesses, PreviousGuess }
    public class SecretNumber
    {

        private int _number;
        private List<int> _previousGuesses = new List<int>();
        public const int MaxNumberOfGuesses = 7;

        public bool CanMakeGuess { get; set; }
        public int Count { get; set; }
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
        public Outcome Outcome { get; set; }
        public ReadOnlyCollection<int> PreviousGuesses
        {
            get { return _previousGuesses.AsReadOnly(); }
        }

        public void Initialize()
        {
            if (PreviousGuesses.Count >= 1) 
            {
                _previousGuesses.Clear();
            }
            CanMakeGuess = true;
            Outcome = Outcome.Indefine;

            Random randomNumber = new Random();
            _number = randomNumber.Next(1, 101);
        }
        public Outcome MakeGuess(int guess)
        {

            if (guess < 1 || guess > 100 || Count > MaxNumberOfGuesses)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (Count >= 1) 
            {
                if (PreviousGuesses.Contains(guess))
                {
                    CanMakeGuess = true;
                    return Outcome.PreviousGuess;
                }
            }
            _previousGuesses.Add(guess);
            Count = PreviousGuesses.Count;
            if (guess == _number)
            {
                CanMakeGuess = false;
                return Outcome.Correct;
            }
            if (Count == MaxNumberOfGuesses) 
            {
                CanMakeGuess = false;
                return Outcome.NoMoreGuesses;
            }
            if (guess < _number)
            {
                CanMakeGuess = true;
                return Outcome.Low;
            }
            if (guess > _number)
            {
                CanMakeGuess = true;
                return Outcome.High;
            }
        }
        public SecretNumber()
        {
            List<int> PreviousGuesses = new List<int>();
            _previousGuesses = PreviousGuesses;
            Initialize();
        }
    }
}