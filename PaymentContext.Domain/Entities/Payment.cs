using System;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public abstract class Payment : Entity
    {
        protected Payment(DateTime paidDate, DateTime exprireDate, decimal total, decimal paidTotal, string payer, Document document, Address address, Email email)
        {
            Number = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper();
            PaidDate = paidDate;
            ExprireDate = exprireDate;
            Total = total;
            PaidTotal = paidTotal;
            Payer = payer;
            Document = document;
            Address = address;
            Email = email;

            AddNotifications(new Contract()
                .Requires()
                .IsLowerOrEqualsThan(0, Total, "Payment.Total", "O total deve ser maior que zero")
                .IsGreaterOrEqualsThan(Total, PaidTotal, "Payment.PaidTotal", "O valor pago não pode ser menor que o valor total")
                );
        }

        public string Number { get; private set; }

        public DateTime PaidDate { get; private set; }

        public DateTime ExprireDate { get; private set; }

        public decimal Total { get; private set; }

        public decimal PaidTotal { get; private set; }

        public string Payer { get; private set; }
        public Document Document { get; private set; }

        public Address Address { get; private set; }

        public Email Email { get; private set; }

    }
}
