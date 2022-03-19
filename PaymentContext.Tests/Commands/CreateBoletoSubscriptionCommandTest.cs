using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class CreateBoletoSubscriptionCommandTest
    {
        // Red, Green, Refactory     

        public CreateBoletoSubscriptionCommandTest()
        {
        }

        // Just an example for testing commands
        [TestMethod]
        public void ShouldReturnErrorWhenStudentNameIsInvalid()
        {
            var boletoCommand = new CreateBoletoSubscriptionCommand();
            boletoCommand.StudentFirstName = "";
            boletoCommand.Validade();
            Assert.IsTrue(!boletoCommand.Valid);
        }
    }
}