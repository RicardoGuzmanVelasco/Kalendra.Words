using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Kalendra.Commons.Runtime.Domain.Services;
using Kalendra.Maths;

namespace Kalendra.Words.Runtime.Domain
{
    public class Thesaurus : IThesaurus
    {
        readonly IRandomService random;
        Dictionary<int, HashSet<string>> words;

        #region Constructors
        public Thesaurus(IEnumerable<string> words, IRandomService random = null) : this(random) => PopulateByWordSize(words);

        Thesaurus(IRandomService random) : this() => this.random = random ?? new SystemRandomService();
        Thesaurus() => PopulateByWordSize(Array.Empty<string>());
        
        void PopulateByWordSize(IEnumerable<string> wordsToPopulateWith)
        {
            words = new Dictionary<int, HashSet<string>>();

            foreach(var word in wordsToPopulateWith)
                AddWord(word);
        }

        void AddWord(string word)
        {
            var key = word.Length;
            if(!words.ContainsKey(key))
                words[key] = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            words[key].Add(word);
        }
        #endregion

        public bool HasWord([NotNull] string word)
        {
            return words.ContainsKey(word.Length) &&
                   words[word.Length].Contains(word);
        }

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
    }
}