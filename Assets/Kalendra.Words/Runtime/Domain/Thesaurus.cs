using System.Collections.Generic;
using System.Linq;

namespace Kalendra.Words.Domain.Kalendra.Words.Runtime.Domain
{
    public class Thesaurus
    {
        readonly List<string> words;

        #region Constructors
        public Thesaurus() => words = new List<string>();
        public Thesaurus(IEnumerable<string> words) : this(words.ToList()) { }
        public Thesaurus(List<string> words) => this.words = words;
        #endregion

        public bool Has(string word)
        {
            return words.Contains(word);
        }
    }
}