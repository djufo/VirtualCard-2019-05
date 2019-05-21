namespace VirtualCard.Lib.Services
{
    public interface IPinService
    {
        string GetHash(int pin);
    }
}