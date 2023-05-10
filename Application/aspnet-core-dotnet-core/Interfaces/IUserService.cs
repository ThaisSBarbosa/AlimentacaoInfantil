using AlimentacaoInfantil.Models;

namespace AlimentacaoInfantil.Interfaces
{
    public interface IUserService
    {
        UserModel Authenticate(string email, string password);
    }
}