using System;
using System.Collections.Generic;
using System.Linq;
using Kalendra.Maths;

namespace Kalendra.Words.Runtime.Domain
{
    public class Thesaurus : IThesaurus
    {
        readonly List<string> words;

        #region Constructors
        public Thesaurus() => words = new List<string>();
        public Thesaurus(IEnumerable<string> words) : this(words.ToList()) { }
        public Thesaurus(List<string> words) => this.words = words;
        #endregion

        public bool HasWord(string word) => words.Contains(word);

        public string GetWord()
        {
            return GetWordOfSize(Interval.From(1, int.MaxValue));
        }

        public string GetWordOfSize(Interval wordSizes)
        {
            //TODO: when Interval supports, select random size from Interval and reuse past method. 
            for(var i = 1; i < int.MaxValue; i++)
                if(wordSizes.Includes(i))
                    return GetWordOfSize(i);
            
            return null;
        }
        
        public string GetWordOfSize(int wordSize)
        {
            if(wordSize < 1)
                throw new ArgumentOutOfRangeException();
            
            return words.FirstOrDefault(w => w.Length == wordSize);
        }
    }
}