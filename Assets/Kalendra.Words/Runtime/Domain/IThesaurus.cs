using Kalendra.Maths;

namespace Kalendra.Words.Runtime.Domain
{
    public interface IThesaurus
    {
        bool HasWord(string word);
        string GetWord();
        
        string GetWordOfSize(int wordSize);
        string GetWordOfSize(Interval wordSizes);
    }
}