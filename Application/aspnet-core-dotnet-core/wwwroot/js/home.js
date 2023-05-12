window.onload = function () {

    autenticaUsuario();
};

function autenticaUsuario() {

    // Usuário mockado
    var userCredentials = {
        Id: 6,
        Nome: "Luana",
        Email: "luana@example.com",
        Senha: "1234"
    };

    $.ajax({
        url: "../api/Autenticacao/AutenticarUsuario_v1",
        method: "POST",
        contentType: 'application/json',
        data: JSON.stringify(userCredentials),
        success: function (dados) {
            sessionStorage.setItem("token", dados.token);
        }
    });
}

