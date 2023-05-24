using AlimentacaoInfantil.DAO;
using AlimentacaoInfantil.Interfaces;
using AlimentacaoInfantil.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace AlimentacaoInfantil.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;

        public UserService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public UsuarioViewModel Authenticate(string nome, string password)
        {
            List<UsuarioViewModel> listaUsuarios;
            UsuarioDAO usuarioDAO = new UsuarioDAO(_config);
            listaUsuarios = usuarioDAO.Lista();

            var user = listaUsuarios.SingleOrDefault(x => x.Nome == nome && x.Senha == password);

            if (user == null)
                return null;

            return user;
        }
    }
}
