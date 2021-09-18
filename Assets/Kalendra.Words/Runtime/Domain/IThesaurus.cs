namespace Kalendra.Words.Runtime.Domain
{
    public interface IThesaurus
    {
        bool HasWord(string word);
        
        string GetWordOfSize(int wordSize);
    }
}