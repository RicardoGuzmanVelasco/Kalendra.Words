using System;
using System.Linq;
using Kalendra.Words.Runtime.Domain;
using Kalendra.Words.Runtime.Infrastructure.Details;
using UnityEngine;

namespace Kalendra.Words.Runtime.Infrastructure
{
    public class ThesaurusLoader
    {
        public Thesaurus Load(string language)
        {
            var file = Resources.Load<TextAsset>(language);
            var reader = new ByteReader(file, ',');
            
            var words = reader.ReadCSV();
            return new Thesaurus(words.Where(w => !string.IsNullOrWhiteSpace(w)));
        }
    }
}