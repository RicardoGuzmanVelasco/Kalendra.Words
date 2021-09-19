using System;
using System.Collections.Generic;
using System.Linq;
using Kalendra.Commons.Runtime.Domain.Services;
using Kalendra.Maths;

namespace Kalendra.Words.Runtime.Domain
{
    public class Thesaurus : IThesaurus
    {
        readonly IRandomService random;
        Dictionary<int, List<string>> words;

        #region Constructors
        public Thesaurus(IEnumerable<string> words, IRandomService random = null) : this(random) => PopulateByWordSize(words);

        Thesaurus(IRandomService random) : this() => this.random = random ?? new SystemRandomService();
        Thesaurus() => words = new Dictionary<int, List<string>>();
        
        void PopulateByWordSize(IEnumerable<string> wordsToPopulateWith)
        {
            words = new Dictionary<int, List<string>>();

            foreach(var word in wordsToPopulateWith)
                AddWord(word);
        }

        void AddWord(string word)
        {
            var key = word.Length;
            if(!words.ContainsKey(key))
                words[key] = new List<string>();

            words[key].Add(word);
        }
        #endregion

        public bool HasWord(string word) => words.ContainsKey(word.Length) && words[word.Length].Contains(word);

        public string GetWord() => words.Any() ? GetWordOfSize(random.GetRandom(words.Keys)) : null;

        public string GetWordOfSize(Interval wordSizes)
        {
            if(wordSizes < 1 || wordSizes.Includes(0))
                throw new ArgumentOutOfRangeException(nameof(wordSizes), "{Precondition} Just positive word sizes");
            
            var feasibleSizes = wordSizes.IterateOver();
            var selectedSize = random.GetRandom(feasibleSizes);
            
            return GetWordOfSize(selectedSize);
        }
        
        public string GetWordOfSize(int wordSize)
        {
            if(wordSize < 1)
                throw new ArgumentOutOfRangeException(nameof(wordSize), "{Precondition} Just positive word sizes");

            return !words.ContainsKey(wordSize) ? null : random.GetRandom(words[wordSize]);
        }

        #region Support
        int MinLenghtOfWords => words.Keys.Min();
        int MaxLenghtOfWords => words.Keys.Max();
        #endregion
    }
}