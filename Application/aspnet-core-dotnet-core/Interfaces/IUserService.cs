using AlimentacaoInfantil.Models;

namespace AlimentacaoInfantil.Interfaces
{
    public interface IUserService
    {
        UsuarioViewModel Authenticate(string email, string password);
    }
}