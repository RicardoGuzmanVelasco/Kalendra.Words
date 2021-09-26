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
    public class AlphabetTests
    {
        static IEnumerable<T2> Repeat<T2>(Func<T2> over, int times) => Interval.From(1, times).IterateOver().Select(_ => over.Invoke());
        
        [Test]
        public void EmptyAlphabet_HasNotLetter()
        {
            ValuedAlphabet<int> sut = Build.Alphabet();

            var result = sut.HasLetter('a');

            result.Should().BeFalse();
        }

        [Test]
        public void PopulatedAlphabet_HasContainedLetters()
        {
            ValuedAlphabet<int> sut = Build.Alphabet().WithLetter('a');

            var result = sut.HasLetter('a');

            result.Should().BeTrue();
        }

        [Test]
        public void ValuedAlphabet_LetsAddLetter_AfterCreated()
        {
            ValuedAlphabet<int> sut = Build.Alphabet();

            sut.AddLetter('a', default);

            var result = sut.HasLetter('a');
            result.Should().BeTrue();
        }

        [Test]
        public void AnyAlphabet_CannotRepeat_SameLetter()
        {
            var sut = Build.Alphabet()
                .WithLetter('a')
                .WithLetter('a');

            Action act = () => sut.Build();

            act.Should().ThrowExactly<ArgumentException>();
        }

        [Test]
        public void ValuedAlphabet_CannotAdd_SameLetterTwice()
        {
            ValuedAlphabet<int> sut = Build.Alphabet().WithLetter('a');

            Action act = () => sut.AddLetter('a', default);

            act.Should().ThrowExactly<ArgumentException>();
        }

        [Test]
        public void AnyAlphabet_CannotReturn_ValueOfNotContainedLetter()
        {
            ValuedAlphabet<int> sut = Build.Alphabet();

            Action act = () => sut.GetValueOf('a');

            act.Should().ThrowExactly<InvalidOperationException>();
        }

        [Test]
        public void AnyAlphabet_AskedForValueOfContainedLetter_ReturnsThatLetterValue()
        {
            ValuedAlphabet<int> sut = Build.Alphabet().WithLetter('a', 1);

            var result = sut.GetValueOf('a');

            result.Should().Be(1);
        }

        [Test]
        public void EmptyAlphabet_CannotReturn_WhenAskedForLetter()
        {
            ValuedAlphabet<int> sut = Build.Alphabet();

            Action act = () => sut.GetLetter();

            act.Should().ThrowExactly<InvalidOperationException>();
        }
        
        [Test]
        public void OneLetterAlphabet_AskedForLetter_ReturnsThatLetter()
        {
            ValuedAlphabet<int> sut = Build.Alphabet().WithLetter('a');

            var result = sut.GetLetter();

            result.Should().Be('a');
        }

        [Test, Retry(10)]
        public void PopulatedAlphabet_AskedForLetter_ReturnsRandomly()
        {
            ValuedAlphabet<int> sut = Build.Alphabet().WithSomeLetters();

            char Act() => sut.GetLetter();

            var resultDifferentLetters = Repeat(Act, 50).Distinct();
            resultDifferentLetters.Should().HaveCountGreaterThan(1);
        }

        [Test]
        public void EmptyAlphabet_AskedForLetterOfAnyValue_ThrowsException()
        {
            ValuedAlphabet<int> sut = Build.Alphabet();

            Action act = () => sut.GetLetterOfValue(0);

            act.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }

        [Test]
        public void PopulatedAlphabet_AskedForNotPresentValue_ThrowsException()
        {
            ValuedAlphabet<int> sut = Build.Alphabet().WithLetter('a');

            Action act = () => sut.GetLetterOfValue(-1);

            act.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }

        [Test]
        public void OneLetterAlphabet_AskedForThatLetterValue_ReturnsThatLetter()
        {
            ValuedAlphabet<int> sut = Build.Alphabet().WithLetter('a', 1);

            var result = sut.GetLetterOfValue(sut.GetValueOf('a'));
            
            result.Should().Be('a');
        }

        [Test]
        public void PopulatedAlphabet_AskedForContainedValue_ReturnsOneLetterWithThatValue()
        {
            ValuedAlphabet<int> sut = Build.Alphabet()
                .WithLetter('a', default)
                .WithLetter('b', default);

            var result = sut.GetLetterOfValue(sut.GetValueOf('a'));
            
            result.Should().BeInRange('a', 'b');
        }

        [Test, Retry(10)]
        public void PopulatedAlphabet_AskedForContainedValue_ReturnsRandomly()
        {
            ValuedAlphabet<int> sut = Build.Alphabet()
                .WithLetter('a', 1)
                .WithLetter('b', 1);

            char Act() => sut.GetLetterOfValue(1);

            var results = Repeat(Act, 50);
            results.Should().Contain('a').And.Contain('b');
        }
    }
}