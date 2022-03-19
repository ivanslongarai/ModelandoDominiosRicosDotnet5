using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class DocumentTests
    {
        // Red, Green, Refactory

        [TestMethod]
        public void ShoudReturnErrorWhenCNPJIsInvalid()
        {
            var doc = new Document("12345", EDocumentType.CNPJ);
            Assert.IsTrue(!doc.Valid);
        }

        [TestMethod]
        public void ShoudReturnSuccessWhenCNPJIsValid()
        {
            var doc = new Document("12345678541254", EDocumentType.CNPJ);
            Assert.IsTrue(doc.Valid);
        }

        [TestMethod]
        public void ShoudReturnErrorWhenCPFIsInvalid()
        {
            var doc = new Document("12345", EDocumentType.CPF);
            Assert.IsTrue(!doc.Valid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("12587451254")]
        [DataRow("36985487215")]
        [DataRow("69854785125")]
        public void ShoudReturnSuccessWhenCPFIsValid(string cpf)
        {
            var doc = new Document(cpf, EDocumentType.CPF);
            Assert.IsTrue(doc.Valid);
        }

    }
}