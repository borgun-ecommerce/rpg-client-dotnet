namespace BorgunPayment.Model
{
    public interface IRPGClient
    {
        IPaymentAPI Payment { get; }

        ITokenMultiAPI TokenMulti { get; }

        ITokenSingleAPI TokenSingle { get; }
    }
}