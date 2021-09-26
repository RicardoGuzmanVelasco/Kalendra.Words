using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Execution;
using Kalendra.Commons.Runtime.Domain.Services;
using Kalendra.Words.Runtime.Infrastructure;
using NUnit.Framework;
using UnityEngine;

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

        [Test, Retry(10)]
        public void TwoThesaurus_LoadedWithSameSeedAndLanguage_ReturnSameWords()
        {
            //Arrange
            var seed = DateTime.Now.GetHashCode();
            IRandomService Random() => new SystemRandomService { Seed = seed };
            
            var loader = new ThesaurusLoader();

            //Act
            var sut1 = loader.Load("Spanish", Random());
            var sut2 = loader.Load("Spanish", Random());

            //Assert
            using(new AssertionScope())
                for(var _ = 0; _ < 50; _++)
                    sut1.GetWord().Should().Be(sut2.GetWord());
        }
    }
}