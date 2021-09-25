namespace Kalendra.Words.Runtime.Domain
{
    public interface IAlphabet<out T>
    {
        bool HasLetter(char letter);
        T GetValueOf(char letter);
        char GetLetter();
    }
}