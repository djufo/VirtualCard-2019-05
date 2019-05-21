using VirtualCard.Lib.Models;

namespace VirtualCard.Lib.Services
{
    public interface ITransactionService
    {
        Response Submit(Request request);
    }
}