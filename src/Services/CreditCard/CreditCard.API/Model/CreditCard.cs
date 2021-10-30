using CreditCard.API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCard.API.Model
{
    public class CreditCard
    {
        public Guid Id { get; private set; }
        public bool Active { get; private set; }
        public string Name { get; private set; }
        public string Number { get; private set; }
        public string VerificationCode { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public EnumFlag Flag { get; private set; }

        protected CreditCard() { }

        public CreditCard(Guid id, string name, bool active, string number, string verificationCode, DateTime expirationDate, EnumFlag flag)
        {
            Id = id;
            Active = active;
            Name = name;
            Number = number;
            VerificationCode = verificationCode;
            ExpirationDate = expirationDate;
            Flag = flag;
        }
    }
}
