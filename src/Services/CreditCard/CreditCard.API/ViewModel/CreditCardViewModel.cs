using System;
using System.ComponentModel.DataAnnotations;

namespace CreditCard.API.ViewModel
{
    public class CreditCardViewModel
    {
        [Required(ErrorMessage = "The Active is required")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "The Name is required")]
        [MinLength(2)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Number is required")]
        [CreditCard(ErrorMessage = "The credit card number is invalid")]
        public string Number { get; set; }

        [Required(ErrorMessage = "The Verification code is required")]
        [MinLength(3)]
        [MaxLength(3)]
        public string VerificationCode { get; set; }

        [Required(ErrorMessage = "The ExpirationDate code is required")]
        [DataType(DataType.Date, ErrorMessage = "ExpirationDate is invalid format")]
        public DateTime ExpirationDate { get; set; }

        [Required(ErrorMessage = "The Flag code is required")]
        public EnumFlag Flag { get; set; }
    }

    public enum EnumFlag
    {
        Visa, Mastercard, Elo
    }
}
