using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kalendra.Maths;
using Kalendra.Words.Runtime.Domain;
using Kalendra.Words.Tests.Builders;
using NUnit.Framework;

namespace Kalendra.Words.Tests.Editor
{
    public class ThesaurusTests
    {
        #region Fixture
        static string SomeWord => "some word";
        static string AnyOtherWord => "anything";

        static IEnumerable<T> Repeat<T>(Func<T> over, int times) => Interval.From(1, times).IterateOver().Select(_ => over.Invoke());
        #endregion
        
        #region Has a word
        [Test]
        public void EmptyThesaurus_HasNot_AnyWord()
        {
            Thesaurus sut = Build.Thesaurus();

            sut.HasWord(SomeWord).Should().BeFalse();
        }

        [Test]
        public void AnyThesaurus_AskedIfHasNullWord_ThrowsException()
        {
            Thesaurus sut = Build.Thesaurus();

            Action act = () => sut.HasWord(null!);

            act.Should().ThrowExactly<NullReferenceException>();
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

        [Test]
        public void Thesaurus_IsCaseInsensitive()
        {
            Thesaurus sut = Build.Thesaurus().WithWords("CASE-sensitive");

            sut.HasWord("case-SENSITIVE").Should().BeTrue();
        }
        #endregion

        #region Get a word
        [Test]
        public void EmptyThesaurus_AskedForAnyWord_ReturnsNull()
        {
            Thesaurus sut = Build.Thesaurus();

            var result = sut.GetWord();

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
        public void OneWordThesaurus_AskedForAnyWord_ReturnsThatWord()
        {
            Thesaurus sut = Build.Thesaurus().WithWords(SomeWord);

            var result = sut.GetWord();

            result.Should().Be(SomeWord);
        }
        
        [Test]
        public void OneWordThesaurus_AskedForThatWordSize_ReturnsThatWord()
        {
            Thesaurus sut = Build.Thesaurus().WithWords(SomeWord);

            var result = sut.GetWordOfSize(SomeWord.Length);

            result.Should().Be(SomeWord);
        }

        [Test, Retry(10)]
        public void PopulatedThesaurus_AskedForWord_DoesNotAlwaysReturnsTheSameWord()
        {
            Thesaurus sut = Build.Thesaurus().WithVarietyOfWords();

            string Act() => sut.GetWord();

            var resultDifferentWords = Repeat(Act, 50).Distinct();
            resultDifferentWords.Should().HaveCountGreaterThan(1);
        }

       [Test]
        public void AnyThesaurus_AskedForWordOfNotContainedSizes_ReturnsNull()
        {
            Thesaurus sut = Build.Thesaurus().WithWords(SomeWord);

            var result = sut.GetWordOfSize(Interval.From(SomeWord.Length + 1));

            result.Should().BeNull();
        }

        [Test, Retry(10)]
        public void PopulatedThesaurus_AskedForAnyWord_DoesNotReturnAlwaysWordsOfTheSameSize()
        {
            Thesaurus sut = Build.Thesaurus().WithVarietyOfWords();

            string Act() => sut.GetWord();

            var resultDifferentLengths = Repeat(Act, 50).Where(s =>s != null).Distinct();
            resultDifferentLengths.Should().HaveCountGreaterThan(1);
        }
        #endregion
    }
}
