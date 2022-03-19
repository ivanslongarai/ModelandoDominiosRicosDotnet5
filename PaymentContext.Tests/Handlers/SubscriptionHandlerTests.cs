using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        // Red, Green, Refactory     
        // ShouldReturnErrorWhen
        // ShouldReturnSuccessWhen

        [TestMethod]
        public void ShouldReturnErrorWhenStudentDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();

            command.StudentFirstName = "Ivan";
            command.StudentLastName = "Longarai";
            command.StudentDocument = "99999999999";
            command.StudentDocumentType = EDocumentType.CPF;
            command.StudentEmailAddress = "ivan2@gmail.com";

            command.BarCode = "12345678910";
            command.BoletoNumber = "123456";
            command.PaymentNumber = "123456";

            command.PaymentPaidDate = DateTime.Now;
            command.PaymentExprireDate = DateTime.Now;
            command.PaymentTotal = 10;
            command.PaymentPaidTotal = 10;

            command.PaymentPayer = "Payer";
            command.PaymentPayerDocumentType = EDocumentType.CPF;
            command.PaymentPayerDocument = "55555555555";
            command.PaymentPayerEmailAdress = "payer@gmail.com";

            command.PaymentAddressStreet = "Andradas";
            command.PaymentAddressNumber = "555";
            command.PaymentAddressNeighborhood = "Centro";
            command.PaymentAddressCity = "Porto Alegre";
            command.PaymentAddressState = "RS";
            command.PaymentAddressCountry = "Brasil";
            command.PaymentAddressZipCode = "125485411";
            command.PaymentAddressComplement = "Perto da Praia";

            handler.Handle(command);
            Assert.AreEqual(false, handler.Valid);

        }

    }
}