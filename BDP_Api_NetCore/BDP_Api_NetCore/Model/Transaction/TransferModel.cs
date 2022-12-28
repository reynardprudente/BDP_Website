namespace BDP_Api_NetCore.Model.Transaction
{
    public class TransferModel
    {
        public long AccountNumberOrigin { get; set; }

        public long AccountNumberDestination { get; set; }

        public double Amount { get; set; }
    }
}
