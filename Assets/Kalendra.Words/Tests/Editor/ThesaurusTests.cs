using System;
using FluentAssertions;
using Kalendra.Words.Domain.Kalendra.Words.Runtime.Domain;
using NUnit.Framework;

namespace Kalendra.Words.Tests.Editor
{
    public class ThesaurusTests
    {
        [Test]
        public void EmptyThesaurus_HasNot_AnyWord()
        {
            var sut = new Thesaurus();

            var result = sut.Has("anyWord");

            result.Should().BeFalse();
        }

        [Test]
        public void ThesaurusWithWord_Has_ThatWord()
        {
            var sut = new Thesaurus(new[] { "containedWord" });

            var result = sut.Has("containedWord");

            result.Should().BeTrue();
        }

        [Test]
        public void ThesaurusWithSomeWord_HasNot_AnyOtherWord()
        {
            var sut = new Thesaurus(new[] { "containedWord" });

            var result = sut.Has("anyotherWord");

            result.Should().BeFalse();
        }

        [Test]
        public void ThesaurusWithSomeWords_Has_AllTheseWords()
        {
            var sut = new Thesaurus(new[] { "containedWord1", "containedWord2" });

            sut.Has("containedWord1").Should().BeTrue();
            sut.Has("containedWord2").Should().BeTrue();
            sut.Has("anyotherWord").Should().BeFalse();
        }
    }
}