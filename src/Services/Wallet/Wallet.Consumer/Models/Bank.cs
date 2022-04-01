namespace Wallet.Consumer.Models
{
    public class Bank
    {
        public EnumBankCode Code { get; set; }
        public string Branch { get; set; }
        public string Account { get; set; }
        public string AccountDigit { get; set; }
    }

    public enum EnumBankCode
    {
        ITAU = 033, BRADESCO = 041, SANTANDER = 011
    }
}
