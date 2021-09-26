using System;
using System.Collections.Generic;
using Kalendra.Commons.Runtime.Domain.Services;
using System.Linq;

namespace Kalendra.Words.Runtime.Domain
{
    public class Abecedary : IAlphabet<int>
    {
        readonly List<string> letters;
        readonly IRandomService random = new SystemRandomService();

        public Abecedary(IEnumerable<string> letters)
        {
            this.letters = new List<string>();
            foreach(var letter in letters)
                if(this.letters.Contains(letter))
                    throw new ArgumentException($"{letter} was repeated");
                else
                    this.letters.Add(letter);
        }

        public bool HasLetter(char letter) => letters.Contains(letter.ToString(), StringComparer.OrdinalIgnoreCase);

        public int GetValueOf(char letter)
        {
            if(!HasLetter(letter))
                throw new InvalidOperationException($"{letter} is not a known letter");
            
            return 1 + letters.IndexOf(letter.ToString());
        }

        public string GetLetterAtPosition(int position)
        {
            position--;
            if(position < 0 || position >= letters.Count)
                throw new ArgumentOutOfRangeException($"{position} is not a letter position");

            return letters[position];
        }

        public char GetLetter() => random.GetRandom(letters[0]);
        public char GetLetterOfValue(int value) => letters[value][0];
    }
}