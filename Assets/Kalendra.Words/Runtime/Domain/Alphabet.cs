using System;
using System.Collections.Generic;
using System.Linq;

namespace Kalendra.Words.Runtime.Domain
{
    public interface IAlphabet
    {
        bool HasLetter(char letter);
        float GetValueOf(char letter);
        char GetLetter();
    }
    
    public class Abecedary : IAlphabet
    {
        List<string> letters;

        public Abecedary(IEnumerable<string> letters) => this.letters = letters.ToList();

        public bool HasLetter(char letter) => letters.Contains(letter.ToString());

        public float GetValueOf(char letter)
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