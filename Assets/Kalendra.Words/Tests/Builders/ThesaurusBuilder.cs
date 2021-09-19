using System;
using System.Collections.Generic;
using System.Linq;
using Kalendra.Words.Runtime.Domain;

namespace Kalendra.Words.Tests.Builders
{
    public class ThesaurusBuilder
    {
        string[] words = Array.Empty<string>();
        #region Fluent API
        public ThesaurusBuilder WithWords(params string[] words)
        {
            this.words = words;
            return this;
        }

        public ThesaurusBuilder WithVarietyOfWords()
        {
            words = new[]
            {
                "a", "rich", "thesaurus",
                "with", "plenty", "of",
                "different", "sized", "words"
            };
            return this;
        }
        #endregion

        #region ObjectMother/FactoryMethods
        ThesaurusBuilder() { }

        public static ThesaurusBuilder New() => new ThesaurusBuilder();
        #endregion

        #region Builder implementation
        public Thesaurus Build() => new Thesaurus(words);
        public static implicit operator Thesaurus(ThesaurusBuilder builder) => builder.Build();
        #endregion
    }
}