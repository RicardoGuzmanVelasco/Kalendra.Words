namespace Kalendra.Words.Runtime.Domain
{
    public interface IAlphabet<T>
    {
        bool HasLetter(char letter);
        T GetValueOf(char letter);
        char GetLetter();
        char GetLetterOfValue(T value);
    }
}