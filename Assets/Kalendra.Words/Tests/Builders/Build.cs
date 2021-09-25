namespace Kalendra.Words.Tests.Builders
{
    public static class Build
    {
        public static ThesaurusBuilder Thesaurus() => ThesaurusBuilder.New();
        public static AbecedaryBuilder Abecedary() => AbecedaryBuilder.New();
        public static ValuedAlphabetBuilder Alphabet() => ValuedAlphabetBuilder.New();
    }
}