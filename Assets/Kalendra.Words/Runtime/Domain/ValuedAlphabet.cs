using System;
using System.Collections.Generic;
using System.Linq;
using Kalendra.Commons.Runtime.Domain.Services;

namespace Kalendra.Words.Runtime.Domain
{
    public class ValuedAlphabet<T> : IAlphabet<T>
    {
        readonly Dictionary<char, T> valuedLetters;
        readonly IRandomService random;

        public ValuedAlphabet(IRandomService random, IEnumerable<(char, T)> valuedLetters)
        {
            this.random = random;
            this.valuedLetters = valuedLetters.ToDictionary(t => t.Item1, t => t.Item2);
        }

        public bool HasLetter(char letter) => valuedLetters.ContainsKey(letter);

        public void AddLetter(char letter, T value)
        {
            if(valuedLetters.ContainsKey(letter))
                throw new ArgumentException($"Alphabet already contains {letter}");
            
            valuedLetters[letter] = value;
        }

        public T GetValueOf(char letter)
        {
            if(!HasLetter(letter))
                throw new InvalidOperationException($"{letter} is not a known letter");

            return valuedLetters[letter];
        }

        public char GetLetter()
        {
            if(!valuedLetters.Any())
                throw new InvalidOperationException("No letters contained");

            return random.GetRandom(valuedLetters.Keys);
        }

        public char GetLetterOfValue(T value)
        {
            if(!valuedLetters.ContainsValue(value))
                throw new ArgumentOutOfRangeException();

            var matchedLetters = valuedLetters.Keys.Where(c => valuedLetters[c].Equals(value));
            return random.GetRandom(matchedLetters);
        }
    }
}