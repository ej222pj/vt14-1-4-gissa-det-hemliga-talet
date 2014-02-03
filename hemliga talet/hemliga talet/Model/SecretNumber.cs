using System;
using System.Collections.Generic;
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
            get { return _number; }
        }
        public Outcome Outcome { get; set; }
        public IEnumerable<int> PreviousGuesses
        {
            get { return _previousGuesses; }
        }

        public void Initialize()
        {     
            Count = 0;
            Random randomNumber = new Random();
            _number = randomNumber.Next(1, 101);
            
        }
        public Outcome MakeGuess(int guess)
        {
            
            if (guess < 1 || guess > 100 || Count > MaxNumberOfGuesses)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                if (guess > _number)
                {
                    return Outcome.High;
                }
                else if (guess < _number)
                {
                    return Outcome.Low;
                }
                else
                {
                    return Outcome.Correct;
                }
            }
        }
        public SecretNumber()
        {
            Initialize();
        }
    }
}