using System.Security.Claims;

namespace BDP.Application.Dto
{
    public class TransactionDTO
    {
        public long AccountNumber { get; set; }

        public double Amount { get; set; }
    }
}
