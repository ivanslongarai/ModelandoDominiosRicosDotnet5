using PaymentContext.Domain.Entities;

namespace PaymentContext.Domain.Repositories
{

    // InfraStructure handles with the implementation

    public interface IStudentRepository
    {
        bool DocumentExists(string document);
        bool EmailExists(string email);
        void CreateSubscription(Student student);
    }
}