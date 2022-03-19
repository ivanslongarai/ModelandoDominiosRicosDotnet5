using System;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class CreditCardPayment : Payment
    {
        public CreditCardPayment(string cardHolderName, string cardNumber, string lastTransactNumber,
            DateTime paidDate, DateTime exprireDate, decimal total, decimal paidTotal, string payer, Document document, Address address, Email email)
                : base(
                    paidDate,
                    exprireDate,
                    total,
                    paidTotal,
                    payer,
                    document,
                    address,
                    email
                )
        {
            CardHolderName = cardHolderName;
            CardNumber = cardNumber;
            LastTransactNumber = lastTransactNumber;
        }

        public string CardHolderName { get; private set; }
        public string CardNumber { get; private set; }
        public string LastTransactNumber { get; private set; }
    }
}