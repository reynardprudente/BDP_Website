namespace BDP.Domain.Entities.Account
{
    public class AccountEntity : BaseEntity
    {
        public long AccountNumber { get; set; }

        public string CreatedBy { get; set; }
    }
}
