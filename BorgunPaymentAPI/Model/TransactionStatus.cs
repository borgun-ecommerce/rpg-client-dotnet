namespace BorgunPayment.Model
{
    public enum TransactionStatus
    {
        Unknown = 1,
        Accepted = 2,
        Uncaptured = 3,
        Captured = 4,
        Declined = 5,
        Cancelled = 6,
        Refunded = 7,
        Error = 8
    }
}