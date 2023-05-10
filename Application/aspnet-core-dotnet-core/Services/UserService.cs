using AlimentacaoInfantil.Interfaces;
using AlimentacaoInfantil.Models;
using System.Collections.Generic;
using System.Linq;

namespace AlimentacaoInfantil.Services
{
    public class UserService : IUserService
    {
        private readonly List<UserModel> _users = new List<UserModel>
        {
            new UserModel { Id = 1, Nome = "teste", Email = "teste@example.com", Senha = "1234" }
        };

        public UserModel Authenticate(string email, string password)
        {
            var user = _users.SingleOrDefault(x => x.Email == email && x.Senha == password);

            if (user == null)
                return null;

            return user;
        }
    }
}
