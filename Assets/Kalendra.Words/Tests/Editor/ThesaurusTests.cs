using System;
using System.Collections.Generic;
using FluentAssertions;
using Kalendra.Maths;
using Kalendra.Words.Runtime.Domain;
using Kalendra.Words.Tests.Builders;
using NSubstitute;
using NUnit.Framework;

namespace Kalendra.Words.Tests.Editor
{
    public class ThesaurusTests
    {
        #region Fixture
        static string SomeWord => "some word";
        static string AnyOtherWord => "anything";

        static int AnyPositiveNumber => new System.Random().Next(1, int.MaxValue);
        #endregion
        
        #region Has a word
        [Test]
        public void EmptyThesaurus_HasNot_AnyWord()
        {
            Thesaurus sut = Build.Thesaurus();

            sut.HasWord(SomeWord).Should().BeFalse();
        }

        [Test]
        public void ThesaurusWithWord_Has_ThatWord()
        {
            Thesaurus sut = Build.Thesaurus().WithWords(SomeWord);

            sut.HasWord(SomeWord).Should().BeTrue();
        }

        [Test]
        public void ThesaurusWithSomeWord_HasNot_AnyOtherWord()
        {
            Thesaurus sut = Build.Thesaurus().WithWords(SomeWord);

            sut.HasWord(AnyOtherWord).Should().BeFalse();
        }

        [Test]
        public void ThesaurusWithSomeWords_Has_AllTheseWords()
        {
            Thesaurus sut = Build.Thesaurus().WithWords("word1", "word2");

            sut.HasWord("word1").Should().BeTrue();
            sut.HasWord("word2").Should().BeTrue();
            sut.HasWord(AnyOtherWord).Should().BeFalse();
        }
        #endregion

        #region Get a word
        [Test]
        public void EmptyThesaurus_AskedForAnySizedWord_ReturnsNull()
        {
            Thesaurus sut = Build.Thesaurus();

            var result = sut.GetWordOfSize(AnyPositiveNumber);

            result.Should().BeNull();
        }

        [TestCase(0), TestCase(-1)]
        public void AnyThesaurus_AskedForNonPositiveSizedWord_ThrowsException(int wordSize)
        {
            Thesaurus sut = Build.Thesaurus();

            Action act = () => sut.GetWordOfSize(wordSize);

            act.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }

        [Test]
        public void AnyThesaurus_AskedForNotContainedSizedWord_ReturnsNull()
        {
            Thesaurus sut = Build.Thesaurus().WithWords(SomeWord);

            var result = sut.GetWordOfSize(int.MaxValue);

            result.Should().BeNull();
        }

        [Test]
        public void OneWordThesaurus_AskedForThatWordSize_ReturnsThatWord()
        {
            Thesaurus sut = Build.Thesaurus().WithWords(SomeWord);

            var result = sut.GetWordOfSize(SomeWord.Length);

            result.Should().Be(SomeWord);
        }

        [Test, Ignore("TODO")]
        public void PopulatedThesaurus_AskedForWord_DoesNotAlwaysReturnsTheSameWord()
        {
            throw new NotImplementedException();
        }
        #endregion
        
        #region Get some words
        [Test]
        public void EmptyThesaurus_AskedForSomeWords_ReturnsNull()
        {
            Thesaurus sut = Build.Thesaurus();

            IEnumerable<string> result = sut.GetWordsBetweenSizes(1, Interval.From(1, 2));

            result.Should().BeNull();
        }
        #endregion
    }
}