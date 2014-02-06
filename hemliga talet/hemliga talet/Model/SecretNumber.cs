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
                return !(Count >= MaxNumberOfGuesses || Outcome == Outcome.Correct);
            }
        }
        public int Count
        {
            get;
            private set;
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
            get { return new ReadOnlyCollection<int>(_previousGuesses); }
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
            //Nollställer Count
            Count = 0;
            //Tar bort alla gamla gissningar
            _previousGuesses.Clear();
        }
        public Outcome MakeGuess(int guess)
        {
            if (CanMakeGuess)
            {
                if (guess >= 1 && guess <= 100)
                {
                    
                    if (PreviousGuesses.Contains(guess))
                    {
                        Outcome = Outcome.PreviousGuess;
                        return Outcome.PreviousGuess;
                    }
                    _previousGuesses.Add(guess);

                    Count++;
                    if (Count == MaxNumberOfGuesses)
                    {
                        Outcome = Outcome.NoMoreGuesses;
                        return Outcome.NoMoreGuesses;
                    }
                    
                    if (guess < _number)
                    {
                        Outcome = Outcome.Low;
                        return Outcome.Low;
                    }
                    else if (guess > _number)
                    {
                        Outcome = Outcome.High;
                        return Outcome.High;
                    }
                    else
                    {
                        Outcome = Outcome.Correct;
                        return Outcome.Correct;
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            else 
            {
                throw new ArgumentOutOfRangeException();
            }
        }
        
    }
}