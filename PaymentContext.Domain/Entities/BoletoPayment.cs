using System;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class BoletoPayment : Payment
    {
        public BoletoPayment(string barCode, string boletoNumber,
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
            BarCode = barCode;
            BoletoNumber = boletoNumber;
        }

        public string BarCode { get; private set; }
        public string BoletoNumber { get; private set; }
    }
}
