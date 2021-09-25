using System;
using System.Linq;
using Kalendra.Words.Runtime.Domain;

namespace Kalendra.Words.Tests.Builders
{
    public class AbecedaryBuilder
    {
        string[] letters = Array.Empty<string>();

        #region Fluent API
        public AbecedaryBuilder WithLetters(params string[] letters)
        {
            this.letters = letters;
            return this;
        }

        public AbecedaryBuilder WithLetters(params char[] letters)
        {
            this.letters = letters.Select(c => c.ToString()).ToArray();
            return this;
        }
        #endregion

        #region ObjectMother/FactoryMethods
        AbecedaryBuilder()
        {
        }

        public static AbecedaryBuilder New() => new AbecedaryBuilder();
        #endregion

        #region Builder implementation
        public Abecedary Build() => new Abecedary(letters);
        public static implicit operator Abecedary(AbecedaryBuilder builder) => builder.Build();
        #endregion
    }
}