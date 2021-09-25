using System.Collections.Generic;
using Kalendra.Commons.Runtime.Domain.Services;
using Kalendra.Words.Runtime.Domain;

namespace Kalendra.Words.Tests.Builders
{
    public class ValuedAlphabetBuilder
    {
        readonly List<(char, int)> valuedLetters = new List<(char, int)>();
        IRandomService random = new SystemRandomService();
        

        #region Fluent API
        public ValuedAlphabetBuilder WithLetter(char letter) => WithLetter(letter, default);
        
        public ValuedAlphabetBuilder WithLetter(char letter, int value)
        {
            valuedLetters.Add((letter, value));
            return this;
        }

        public ValuedAlphabetBuilder WithRandom(IRandomService random)
        {
            this.random = random;
            return this;
        }
        #endregion

        #region ObjectMother/FactoryMethods
        ValuedAlphabetBuilder() { }

        public static ValuedAlphabetBuilder New() => new ValuedAlphabetBuilder();
        public ValuedAlphabetBuilder WithSomeLetters() => WithLetter('a', 1).WithLetter('b', 2);
        #endregion

        #region Builder implementation
        public ValuedAlphabet<int> Build() => new ValuedAlphabet<int>(random, valuedLetters);
        public static implicit operator ValuedAlphabet<int>(ValuedAlphabetBuilder builder) => builder.Build();
        #endregion
    }
}