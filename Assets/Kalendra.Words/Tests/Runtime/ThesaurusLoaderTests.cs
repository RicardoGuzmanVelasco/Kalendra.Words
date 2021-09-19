using System.Collections.Generic;
using FluentAssertions;
using Kalendra.Words.Runtime.Infrastructure;
using NUnit.Framework;

namespace Kalendra.Words.Tests.Runtime
{
    public class ThesaurusLoaderTests
    {
        #region Fixture
        static IEnumerable<string> Languages()
        {
            yield return "Danish";
            yield return "Dutch";
            yield return "English";
            yield return "French";
            yield return "German";
            yield return "Italian";
            yield return "Norwegian";
            yield return "Portuguese";
            yield return "Spanish";
            yield return "Swedish";
        }
        #endregion
        
        [TestCaseSource(nameof(Languages))]
        public void AnyThesaurus_LoadedFromRequiredLanguages_HasAtLeastAWord(string language)
        {
            var loader = new ThesaurusLoader();

            var result = loader.Load(language);
            
            result.GetWord().Should().NotBeNullOrWhiteSpace();
        }
        
        [TestCaseSource(nameof(Languages))]
        public void AnyThesaurus_LoadedFromRequiredLanguages_HasNotEmptyWords(string language)
        {
            var loader = new ThesaurusLoader();

            var result = loader.Load(language);

            result.HasWord(string.Empty).Should().BeFalse();
        }
    }
}