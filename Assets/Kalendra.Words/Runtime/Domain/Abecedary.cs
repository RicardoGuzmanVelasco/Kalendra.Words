using System;
using System.Collections.Generic;
using System.Linq;

namespace Kalendra.Words.Runtime.Domain
{
    public class Abecedary : IAlphabet<int>
    {
        readonly List<string> letters;

        public Abecedary(IEnumerable<string> letters) => this.letters = letters.ToList();

        public bool HasLetter(char letter) => letters.Contains(letter.ToString());

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

        public char GetLetter() => throw new NotImplementedException();
    }
}