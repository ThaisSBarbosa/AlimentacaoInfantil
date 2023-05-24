function autenticarUsuario() {

    var obj = {
        Nome: $("#userName").val(),
        Senha: $("#userPassword").val()
    };

    $.ajax({
        contentType: "application/json",
        url: "../api/UsuarioAPI/AutenticarUsuario_v1",
        method: "POST",
        data: JSON.stringify(obj),
        success: function (dados) {

            sessionStorage.setItem("token", dados);
            alert('Login realizado!');     
            window.location = "/home/index";
        },
        error: function () {
            alert('Erro');
        }
    });
}

