using System;
using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable,
        IHandler<CreateBoletoSubscriptionCommand>,
        IHandler<CreatePayPalSubscriptionCommand>,
        IHandler<CreateCreditCardSubscriptionCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository studentRepository,
        IEmailService emailService)
        {
            _studentRepository = studentRepository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            // Fail Fast Validation
            command.Validade();

            if (command.Invalid)
                return new CommandResult(false, "Não foi possível realizar a assinatura.");

            if (_studentRepository.EmailExists(command.StudentEmailAddress))
            {
                AddNotification("StudentRepository.EmailExists", "Email já existe");
                return new CommandResult(false, "Email já existe");
            }

            if (_studentRepository.DocumentExists(command.StudentDocument))
            {
                AddNotification("StudentRepository.DocumentExists", "Documento já existe");
                return new CommandResult(false, "Documento já existe.");
            }

            // Create Values Objects
            var name = new Name(command.StudentFirstName, command.StudentLastName);
            var studentDocument = new Document(command.StudentDocument, command.StudentDocumentType);
            var email = new Email(command.StudentEmailAddress);

            var address = new Address(
                    command.PaymentAddressStreet,
                    command.PaymentAddressNumber,
                    command.PaymentAddressNeighborhood,
                    command.PaymentAddressCity,
                    command.PaymentAddressState,
                    command.PaymentAddressCountry,
                    command.PaymentAddressZipCode,
                    command.PaymentAddressComplement
            );

            // Create Entities
            var student = new Student(name, studentDocument, email);
            var subscription = new Subscription(command.PaymentExprireDate);
            var paymentDocument = new Document(command.PaymentPayerDocument, command.PaymentPayerDocumentType);

            var payment = new BoletoPayment(
                    command.BarCode,
                    command.PaymentNumber,
                    command.PaymentPaidDate,
                    command.PaymentExprireDate,
                    command.PaymentTotal,
                    command.PaymentPaidTotal,
                    command.PaymentPayer,
                    paymentDocument,
                    address,
                    email
            );

            // Relationships
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Group Validations
            AddNotifications(name, studentDocument, paymentDocument, address, email, subscription, payment);

            // Check Validations
            if (Invalid)
                return new CommandResult(false, "Não foi possível realizar assinatura");

            // Save data
            _studentRepository.CreateSubscription(student);

            // Send welcome email
            _emailService.Send(command.StudentEmailAddress, student.ToString(), "Bem vindo aos cursos",
                    @"
                    Bem vinda a nossos cursos!
                    Sua assinatura foi criada");

            // Return informations
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            // Fail Fast Validation
            command.Validade();

            if (command.Invalid)
                return new CommandResult(false, "Não foi possível realizar a assinatura.");

            if (_studentRepository.EmailExists(command.StudentEmailAddress))
                return new CommandResult(false, "Este e-mail já está em uso.");

            if (_studentRepository.DocumentExists(command.PaymentPayerDocument))
                return new CommandResult(false, "Este documento já está em uso.");

            // Create Values Objects
            var name = new Name(command.StudentFirstName, command.StudentLastName);
            var studentDocument = new Document(command.StudentDocument, command.StudentDocumentType);
            var email = new Email(command.StudentEmailAddress);

            var address = new Address(
                    command.PaymentAddressStreet,
                    command.PaymentAddressNumber,
                    command.PaymentAddressNeighborhood,
                    command.PaymentAddressCity,
                    command.PaymentAddressState,
                    command.PaymentAddressCountry,
                    command.PaymentAddressZipCode,
                    command.PaymentAddressComplement
            );

            // Create Entities
            var student = new Student(name, studentDocument, email);
            var subscription = new Subscription(command.PaymentExprireDate);
            var paymentDocument = new Document(command.PaymentPayerDocument, command.PaymentPayerDocumentType);

            var payment = new PayPalPayment(
                    command.TransactionCode,
                    command.PaymentPaidDate,
                    command.PaymentExprireDate,
                    command.PaymentTotal,
                    command.PaymentPaidTotal,
                    command.PaymentPayer,
                    paymentDocument,
                    address,
                    email
            );

            // Relationships
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Group Validations
            AddNotifications(name, studentDocument, paymentDocument, address, email, subscription, payment);

            // Check Validations
            if (Invalid)
                return new CommandResult(false, "Não foi possível realizar assinatura");

            // Save data
            _studentRepository.CreateSubscription(student);

            // Send welcome email
            _emailService.Send(command.StudentEmailAddress, student.ToString(), "Bem vindo aos cursos",
                    @"
                    Bem vinda a nossos cursos!
                    Sua assinatura foi criada");

            // Return informations
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
            // Fail Fast Validation
            command.Validade();

            if (command.Invalid)
                return new CommandResult(false, "Não foi possível realizar a assinatura.");

            if (_studentRepository.EmailExists(command.StudentEmailAddress))
                return new CommandResult(false, "Este e-mail já está em uso.");

            if (_studentRepository.DocumentExists(command.PaymentPayerDocument))
                return new CommandResult(false, "Este documento já está em uso.");

            // Create Values Objects
            var name = new Name(command.StudentFirstName, command.StudentLastName);
            var studentDocument = new Document(command.StudentDocument, command.StudentDocumentType);
            var email = new Email(command.StudentEmailAddress);

            var address = new Address(
                    command.PaymentAddressStreet,
                    command.PaymentAddressNumber,
                    command.PaymentAddressNeighborhood,
                    command.PaymentAddressCity,
                    command.PaymentAddressState,
                    command.PaymentAddressCountry,
                    command.PaymentAddressZipCode,
                    command.PaymentAddressComplement
            );

            // Create Entities
            var student = new Student(name, studentDocument, email);
            var subscription = new Subscription(command.PaymentExprireDate);
            var paymentDocument = new Document(command.PaymentPayerDocument, command.PaymentPayerDocumentType);

            var payment = new CreditCardPayment(
                    command.CardHolderName,
                    command.CardNumber,
                    command.LastTransactNumber,
                    command.PaymentPaidDate,
                    command.PaymentExprireDate,
                    command.PaymentTotal,
                    command.PaymentPaidTotal,
                    command.PaymentPayer,
                    paymentDocument,
                    address,
                    email
            );

            // Relationships
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Group Validations
            AddNotifications(name, studentDocument, paymentDocument, address, email, subscription, payment);

            // Check Validations
            if (Invalid)
                return new CommandResult(false, "Não foi possível realizar assinatura");

            // Save data
            _studentRepository.CreateSubscription(student);

            // Send welcome email
            _emailService.Send(command.StudentEmailAddress, student.ToString(), "Bem vindo aos cursos",
                    @"
                    Bem vinda a nossos cursos!
                    Sua assinatura foi criada");

            // Return informations
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
    }
}