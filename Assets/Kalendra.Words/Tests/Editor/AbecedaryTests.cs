using System;
using FluentAssertions;
using Kalendra.Words.Runtime.Domain;
using Kalendra.Words.Tests.Builders;
using NUnit.Framework;

namespace Kalendra.Words.Tests.Editor
{
    public class AbecedaryTests
    {
        #region Fixture
        static char AnyLetter => 'x';
        static char SomeLetter => 'a';
        #endregion
        
        [Test]
        public void AnyEmptyAbecedary_HasNotAnyLetter()
        {
            Abecedary sut = Build.Abecedary();

            var result = sut.HasLetter(AnyLetter);

            result.Should().BeFalse();
        }

        [Test]
        public void AnyPopulatedAbecedary_HasNotLetter_IfWasNotPassedWhenCreated()
        {
            Abecedary sut = Build.Abecedary().WithLetters('a', 'b');

            var result = sut.HasLetter('c');

            result.Should().BeFalse();
        }

        [Test]
        public void AnyPopulatedAbecedary_HasLetter_IfWasPassedWhenCreated()
        {
            Abecedary sut = Build.Abecedary().WithLetters(SomeLetter);

            var result = sut.HasLetter(SomeLetter);

            result.Should().BeTrue();
        }

        [Test]
        public void AnyOneLetterAbecedary_ThatLetterValue_Is1()
        {
            Abecedary sut = Build.Abecedary().WithLetters(SomeLetter);

            var resultValue = sut.GetValueOf(SomeLetter);

            resultValue.Should().Be(1);
        }

        [Test]
        public void AnyPopulatedAbecedary_LetterValue_StandsFromLetterPosition()
        {
            Abecedary sut = Build.Abecedary().WithLetters('a' ,'b', 'c');

            var resultValue2 = sut.GetValueOf('b');
            var resultValue3 = sut.GetValueOf('c');

            resultValue2.Should().Be(2);
            resultValue3.Should().Be(3);
        }

        [Test]
        public void AnyAbecedary_GettingValueOfNotContainedLetter_ThrowsException()
        {
            Abecedary sut = Build.Abecedary();

            Action act = () => sut.GetValueOf(AnyLetter);

            act.Should().ThrowExactly<InvalidOperationException>();
        }

        [Test]
        public void AnyAbecedary_GettingLetterAtPositionZero_ThrowsException()
        {
            Abecedary sut = Build.Abecedary();

            Action act = () => sut.GetLetterAtPosition(0);

            act.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }

        [Test]
        public void AnyAbecedary_GettingLetterAtCountPosition_ReturnsLastLetter()
        {
            Abecedary sut = Build.Abecedary().WithLetters('a', 'b', 'c');

            var result = sut.GetLetterAtPosition(3);

            result.Should().Be('c'.ToString());
        }
    }
}