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

        public string GetWordOfSize(int wordSize)
        {
            if(wordSize < 1)
                throw new ArgumentOutOfRangeException();
            
            return words.FirstOrDefault(w => w.Length == wordSize); //TODO: reuse GetWordsBetweenSizes.
        }

        public IEnumerable<string> GetWordsBetweenSizes(int requestedWordsCount, Interval wordsSizes)
        {
            if(requestedWordsCount < 1)
                throw new ArgumentOutOfRangeException();
            
            var wordsToReturn = new List<string>();
            
            return wordsToReturn;
        }
        
        public IEnumerable<string> GetWordsOfSize(int requestedWordsCount, int wordsSize)
        {
            return GetWordsBetweenSizes(requestedWordsCount, (wordsSize, wordsSize));
        }
    }
}