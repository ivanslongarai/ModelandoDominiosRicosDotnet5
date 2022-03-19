using System;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class PayPalPayment : Payment
    {
        public PayPalPayment(string transactionCode,
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
            TransactionCode = transactionCode;
        }

        public string TransactionCode { get; private set; }
    }
}