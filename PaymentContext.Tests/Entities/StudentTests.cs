using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class StudentTests
    {
        // Red, Green, Refactory     

        private readonly Name _name;
        private readonly Document _document;
        private readonly Email _email;
        private readonly Address _address;
        private readonly Subscription _subscription;
        private readonly Student _student;

        public StudentTests()
        {
            _name = new Name("Ivan", "Longarai");
            _document = new Document("12345678974", EDocumentType.CPF);
            _email = new Email("test@gmail.com");
            _student = new Student(_name, _document, _email);
            _address = new Address("Andradas", "123", "Centro", "Porto Alegre", "RS", "Brasil", "92032500", "Apto");
            _subscription = new Subscription();
        }


        [TestMethod]
        public void ShouldReturnErrorWhenHaveActiveSubscription()
        {
            var payment = new PayPalPayment("ABCD", DateTime.Now, DateTime.Now.AddDays(10), 10, 10, "Payer", _document, _address, _email);
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Invalid);
        }

        public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
        {
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription()
        {
            var payment = new PayPalPayment("ABCD", DateTime.Now, DateTime.Now.AddDays(10), 10, 10, "Payer", _document, _address, _email);
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Valid);
        }

    }
}