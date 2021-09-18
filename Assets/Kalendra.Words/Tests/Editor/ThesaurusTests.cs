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
        static Interval AnyPositiveInterval => Interval.From(1, AnyPositiveNumber);
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

        [Test, Ignore("TODO: random shuffle needed")]
        public void PopulatedThesaurus_AskedForWord_DoesNotAlwaysReturnsTheSameWord()
        {
            throw new NotImplementedException();
        }
        #endregion
        
        #region Get some words
        [Test]
        public void EmptyThesaurus_AskedForSomeWords_ReturnsEmpty()
        {
            Thesaurus sut = Build.Thesaurus();

            var result = sut.GetWordsBetweenSizes(AnyPositiveNumber, AnyPositiveInterval);

            result.Should().BeEmpty();
        }
        
        [Test, Ignore("TODO: Interval lacks support for negative/positive sense or < > operators")]
        public void AnyThesaurus_AskedSomeForNonPositiveSizedWords_ThrowsException()
        {
            Thesaurus sut = Build.Thesaurus();

            Action act = () => sut.GetWordsBetweenSizes(AnyPositiveNumber, (-1, 0));

            act.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
        
        [TestCase(-1), TestCase(0)]
        public void AnyThesaurus_AskedSomeForNonPositiveAmountOfWords_ThrowsException(int howmanyWords)
        {
            Thesaurus sut = Build.Thesaurus();

            Action act = () => sut.GetWordsBetweenSizes(howmanyWords, AnyPositiveInterval);

            act.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
        
        [Test]
        public void AnyThesaurus_AskedForNotContainedSizes_ReturnsNull()
        {
            Thesaurus sut = Build.Thesaurus().WithWords(SomeWord);

            var result = sut.GetWordsBetweenSizes(1, (2, int.MaxValue));

            result.Should().BeEmpty();
        }

        [Test]
        public void AnyThesaurus_AskedForSomeWords_DoesNotRepeatAnyWord()
        {
            Thesaurus sut = Build.Thesaurus().WithWords("wordOfSameSize1", "wordOfSameSize2");

            var result = sut.GetWordsOfSize(2, "wordOfSameSize1".Length);

            result.Should().HaveCount(2);
            result.Should().Contain("wordOfSameSize1");
            result.Should().Contain("wordOfSameSize2");
        }
        
        [Test, Ignore("TODO: random shuffle needed")]
        public void AnyThesaurus_AskedForSomeDiverseWordSizes_ReturnWordsOfTheseSizes()
        {
            Thesaurus sut = Build.Thesaurus().WithWords("long", "longer", "longest");

            var requestedSizesInterval = ("long".Length, "longest".Length);
            var result = sut.GetWordsBetweenSizes(1, requestedSizesInterval);

            var expectedResults = new[] { "long", "longer", "longest" };
            result.Should().Contain(expectedResults);
        }
        #endregion
    }
}