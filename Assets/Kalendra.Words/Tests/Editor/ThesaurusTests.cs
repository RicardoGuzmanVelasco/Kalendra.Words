using FluentAssertions;
using Kalendra.Words.Runtime.Domain;
using Kalendra.Words.Tests.Builders;
using NUnit.Framework;

namespace Kalendra.Words.Tests.Editor
{
    public class ThesaurusTests
    {
        #region Fixture
        static string SomeWord => "something";
        static string AnyOtherWord => "anything";
        #endregion
        
        [Test]
        public void EmptyThesaurus_HasNot_AnyWord()
        {
            Thesaurus sut = Build.Thesaurus();

            sut.Has(SomeWord).Should().BeFalse();
        }

        [Test]
        public void ThesaurusWithWord_Has_ThatWord()
        {
            Thesaurus sut = Build.Thesaurus().WithWords(SomeWord);

            sut.Has(SomeWord).Should().BeTrue();
        }

        [Test]
        public void ThesaurusWithSomeWord_HasNot_AnyOtherWord()
        {
            Thesaurus sut = Build.Thesaurus().WithWords(SomeWord);

            sut.Has(AnyOtherWord).Should().BeFalse();
        }

        [Test]
        public void ThesaurusWithSomeWords_Has_AllTheseWords()
        {
            Thesaurus sut = Build.Thesaurus().WithWords("word1", "word2");

            sut.Has("word1").Should().BeTrue();
            sut.Has("word2").Should().BeTrue();
            sut.Has(AnyOtherWord).Should().BeFalse();
        }
    }
}