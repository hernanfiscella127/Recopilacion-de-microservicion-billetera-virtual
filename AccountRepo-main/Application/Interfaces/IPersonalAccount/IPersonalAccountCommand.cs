using Domain.Models;

namespace Application.Interfaces.IPersonalAccount
{
    public interface IPersonalAccountCommand
    {
        Task InsertPersonalAccount(PersonalAccount account);
        Task DeletePersonalAccount(Guid id);
    }
}
