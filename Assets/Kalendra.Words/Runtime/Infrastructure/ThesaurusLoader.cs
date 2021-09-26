using System;
using System.Linq;
using JetBrains.Annotations;
using Kalendra.Commons.Runtime.Domain.Services;
using Kalendra.Words.Runtime.Domain;
using Kalendra.Words.Runtime.Infrastructure.Details;
using UnityEngine;

namespace Kalendra.Words.Runtime.Infrastructure
{
    public class ThesaurusLoader
    {
        public Thesaurus Load(string language) => Load(language, null!);

        public Thesaurus Load(string language, [NotNull] IRandomService random)
        {
            var file = Resources.Load<TextAsset>(language);
            var reader = new ByteReader(file, ',');
            
            var rawWords = reader.ReadCSV();
            var words = rawWords.Where(w => !string.IsNullOrWhiteSpace(w));
            
            return new Thesaurus(words, random);
        }
    }
}