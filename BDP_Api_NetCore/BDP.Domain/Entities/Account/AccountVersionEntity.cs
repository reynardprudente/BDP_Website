namespace BDP.Domain.Entities.Account
{
    public class AccountVersionEntity : BaseEntity
    {
        public AccountEntity Account { get; set; }

        public double Amount { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModiefiedDate { get; set; }
    }
}
