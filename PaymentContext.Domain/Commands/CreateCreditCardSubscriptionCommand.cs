using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.Commands;

namespace PaymentContext.Domain.Commands
{
    public class CreateCreditCardSubscriptionCommand : Notifiable, ICommand
    {
        // Student
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string StudentDocument { get; set; }
        public EDocumentType StudentDocumentType { get; set; }
        public string StudentEmailAddress { get; set; }

        // CreditCardPayment
        public string CardHolderName { get; private set; }
        public string CardNumber { get; private set; }
        public string LastTransactNumber { get; private set; }

        //Payment
        public string PaymentNumber { get; set; }
        public DateTime PaymentPaidDate { get; set; }
        public DateTime PaymentExprireDate { get; set; }
        public decimal PaymentTotal { get; set; }
        public decimal PaymentPaidTotal { get; set; }
        public string PaymentPayer { get; set; }
        public EDocumentType PaymentPayerDocumentType { get; set; }
        public string PaymentPayerDocument { get; set; }

        //Payment Email
        public string PaymentPayerEmailAdress { get; set; }

        //Payment Adress
        public string PaymentAddressStreet { get; set; }
        public string PaymentAddressNumber { get; set; }
        public string PaymentAddressNeighborhood { get; set; }
        public string PaymentAddressCity { get; set; }
        public string PaymentAddressState { get; set; }
        public string PaymentAddressCountry { get; set; }
        public string PaymentAddressZipCode { get; set; }
        public string PaymentAddressComplement { get; set; }
       
        // Fail Fast Validation
        public void Validade()
        {
            AddNotifications(new Contract()
                    .Requires()
                    .HasMinLen(StudentFirstName, 3, "StudentFirstName.FirstName", "Nome deve conter no mínimo 3 caracteres")
                    .HasMaxLen(StudentFirstName, 80, "StudentFirstName.FirstName", "Nome deve conter no máximo 80 caracteres")
                    .HasMinLen(StudentLastName, 3, "StudentFirstName.LastName", "Sobrenome deve conter no mínimo 3 caracteres")
                    .HasMaxLen(StudentLastName, 120, "StudentFirstName.LastName", "Sobrenome deve conter no máximo 120 caracteres")
                );
        }

    }
}