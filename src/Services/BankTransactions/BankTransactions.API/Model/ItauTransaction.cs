namespace BankTransactions.API.Model
{
    public class ItauTransaction : BaseBankTransaction
    {
        public StatusTransaction StatusTransaction { get; set; }
    }

    public enum StatusTransaction
    {
        CONFIRMED, WAITING, NOT_FOUND
    }
}
