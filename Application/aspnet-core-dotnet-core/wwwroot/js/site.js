$(function () {

    autenticaUsuario();

    $("#btnExecutar").click(function () {
        executaAPI()
    })
});

function executaAPI() {
    $("#txtRequest").html("");
    $("#txtResponse").html("");
    var request = $("#cbAPI").val();
    if (request > 0)
        switch (request) {
            case "1":
                exibirPosts();
                break;
            case "2":
                fazerPost();
                break;
            case "3":
                editarPost();
                break;
            case "4":
                apagarPost();
                break;
            case "5":
                exibirAnuncios();
                break;
            case "6":
                fazerAnuncio();
                break;
            case "7":
                editarAnuncio();
                break;
            case "8":
                apagarAnuncio();
                break;
            case "9":
                enviarAmei();
                break;
            case "10":
                retirarAmei();
                break;
            case "11":
                enviarMensagem();
                break;
            case "12":
                responderMensagem();
                break;
            case "13":
                conectarSePaiMae()
        }
}

function autenticaUsuario() {

    // Usuário mockado
    var userCredentials = {
        Id: 1,
        Nome: "teste",
        Email: "teste@example.com",
        Senha: "1234"
    };

    $.ajax({
        url: "api/Autenticacao/AutenticarUsuario_v1",
        method: "POST",
        contentType: 'application/json',
        data: JSON.stringify(userCredentials),
        success: function (dados) {
            sessionStorage.setItem("token", dados.token);
        }
    });
}

function exibirPosts() {
    var request = {
        url: "api/Posts/ExibirPosts_v1",
        type: "get",
        dataType: "json"
    };

    $.ajax({
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem("token")
        },
        url: "api/Posts/ExibirPosts_v1",
        method: "GET",
        dataType: "json",
        success: function (dados) {
            $("#txtRequest").html(JSON.stringify(request, undefined, 4));
            $("#txtResponse").html(JSON.stringify(dados, undefined, 4))
        }
    });
}

function fazerPost() {
    var request = {
        url: "Posts/FazerPost",
        type: "post",
        data: '{ conteudo: "Post teste", autor: 2, amei: 0, anuncio: false }',
        dataType: "json"
    };

    var obj = {
        conteudo: "Post teste",
        autor: 2,
        amei: 0,
        anuncio: false
    };

    $.ajax({
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem("token")
        },
        contentType: "application/json",
        url: "api/Posts/FazerPost_v1",
        method: "POST",
        data: JSON.stringify(obj),
        success: function (dados) {
            $("#txtRequest").html(JSON.stringify(request, undefined, 4));
            $("#txtResponse").html(JSON.stringify(dados, undefined, 4))
        }
    });
}

function editarPost() {
    var request = {
        url: "api/Posts/EditarPost_v1",
        type: "put",
        data: '{ codigo: [o primeiro registro do banco], conteudo: "novo conteúdo" }',
        dataType: "json"
    };

    var obj = {
        codigo: 1,
        conteudo: "novo conteúdo"
    };

    $.ajax({
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem("token")
        },
        contentType: "application/json",
        url: "api/Posts/EditarPost_v1",
        method: "PUT",
        data: JSON.stringify(obj),
        success: function (dados) {
            $("#txtRequest").html(JSON.stringify(request, undefined, 4));
            $("#txtResponse").html(JSON.stringify(dados, undefined, 4))
        }
    })
}

function apagarPost() {
    var request = {
        url: "Posts/ApagarPost",
        type: "delete",
        data: "{ codigo: [o primeiro registro do banco] }",
        dataType: "json"
    };

    var obj = {
        codigo: 1
    };

    $.ajax({
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem("token")
        },
        contentType: "application/json",
        url: "api/Posts/ApagarPost_v1", 
        method: "DELETE",
        data: JSON.stringify(obj),
        success: function (dados) {
            $("#txtRequest").html(JSON.stringify(request, undefined, 4));
            $("#txtResponse").html(JSON.stringify(dados, undefined, 4))
        }
    })
}

function exibirAnuncios() {

    var request = {
        url: "Posts/ExibirAnuncios",
        type: "get",
        dataType: "json"
    };

    $.ajax({
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem("token")
        },
        url: "api/Posts/ExibirAnuncios_v1",
        method: "GET",
        dataType: "json",
        success: function (dados) {
            $("#txtRequest").html(JSON.stringify(request, undefined, 4));
            $("#txtResponse").html(JSON.stringify(dados, undefined, 4))
        }
    });

}

function fazerAnuncio() {
    var request = {
        url: "Posts/FazerAnuncio",
        type: "post",
        data: '{ conteudo: "Anúncio teste", autor: 1, amei: 0 }',
        dataType: "json"
    };

    var obj = {
        conteudo: "Anúncio teste",
        autor: 1,
        amei: 0,
        anuncio: true
    };

    $.ajax({
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem("token")
        },
        contentType: "application/json",
        url: "api/Posts/FazerAnuncio_v1",
        method: "POST",
        data: JSON.stringify(obj),
        success: function (dados) {
            $("#txtRequest").html(JSON.stringify(request, undefined, 4));
            $("#txtResponse").html(JSON.stringify(dados, undefined, 4))
        }
    });
}

function editarAnuncio() {
    var request = {
        url: "Posts/EditarAnuncio",
        type: "put",
        data: '{ codigo: [o primeiro registro do banco], conteudo: "novo conteúdo 2" }',
        dataType: "json"
    };

    var obj = {
        codigo: 1,
        conteudo: "novo conteúdo 2"
    };

    $.ajax({
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem("token")
        },
        contentType: "application/json",
        url: "api/Posts/EditarAnuncio_v1",
        method: "PUT",
        data: JSON.stringify(obj),
        success: function (dados) {
            $("#txtRequest").html(JSON.stringify(request, undefined, 4));
            $("#txtResponse").html(JSON.stringify(dados, undefined, 4))
        }
    })
}

function apagarAnuncio() {
    var request = {
        url: "Posts/ApagarAnuncio",
        type: "delete",
        data: "{ codigo: [o primeiro registro do banco] }",
        dataType: "json"
    };

    var obj = {
        codigo: 1
    };

    $.ajax({
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem("token")
        },
        contentType: "application/json",
        url: "api/Posts/ApagarAnuncio_v1",
        method: "DELETE",
        data: JSON.stringify(obj),
        success: function (dados) {
            $("#txtRequest").html(JSON.stringify(request, undefined, 4));
            $("#txtResponse").html(JSON.stringify(dados, undefined, 4))
        }
    })
}

function enviarAmei() {
    var request = {
        url: "Posts/EnviarAmei",
        type: "post",
        data: '{ codigo: [o primeiro registro do banco] }',
        dataType: "json"
    };

    var obj = {
        codigo: 1
    };

    $.ajax({
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem("token")
        },
        contentType: "application/json",
        url: "api/Posts/EnviarAmei_v1",
        method: "PUT",
        data: JSON.stringify(obj),
        success: function (dados) {
            $("#txtRequest").html(JSON.stringify(request, undefined, 4));
            $("#txtResponse").html(JSON.stringify(dados, undefined, 4))
        }
    })
}

function retirarAmei() {
    var request = {
        url: "Posts/RetirarAmei",
        type: "put",
        data: '{ codigo: [o primeiro registro do banco] }',
        dataType: "json"
    };

    var obj = {
        codigo: 1
    };

    $.ajax({
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem("token")
        },
        contentType: "application/json",
        url: "api/Posts/RetirarAmei_v1",
        method: "PUT",
        data: JSON.stringify(obj),
        success: function (dados) {
            $("#txtRequest").html(JSON.stringify(request, undefined, 4));
            $("#txtResponse").html(JSON.stringify(dados, undefined, 4))
        }
    })
}

function enviarMensagem() {
    var request = {
        url: "Mensagens/EnviarMensagem",
        type: "post",
        data: '{ conteudo: "Envia mensagem", remetente: 1, destinatario: 2 }',
        dataType: "json"
    };

    var obj = {
        conteudo: "Envia mensagem",
        remetente: 1,
        destinatario: 2
    };

    $.ajax({
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem("token")
        },
        contentType: "application/json",
        url: "api/MensagensAPI/EnviarMensagem_v1",
        method: "POST",
        data: JSON.stringify(obj),
        success: function (dados) {
            $("#txtRequest").html(JSON.stringify(request, undefined, 4));
            $("#txtResponse").html(JSON.stringify(dados, undefined, 4))
        }
    });
}

function responderMensagem() {
    var request = {
        url: "Mensagens/ResponderMensagem",
        type: "post",
        data: '{ codigo: 1, conteudo: "Respondendo mensagem", remetente: 2, destinatario: 1 }',
        dataType: "json"
    };

    var obj = {
        codigo: 1,
        conteudo: "Respondendo mensagem",
        remetente: 2,
        destinatario: 1
    };

    $.ajax({
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem("token")
        },
        contentType: "application/json",
        url: "api/MensagensAPI/ResponderMensagem_v1",
        method: "POST",
        data: JSON.stringify(obj),
        success: function (dados) {
            $("#txtRequest").html(JSON.stringify(request, undefined, 4));
            $("#txtResponse").html(JSON.stringify(dados, undefined, 4))
        }
    });
}

function conectarSePaiMae() {
    var request = {
        url: "Conexoes/ConectarSeAUmPai",
        type: "post",
        data: "{usuario1: [usuario criado para este teste], usuario2: [usuario criado para este teste]}",
        dataType: "json"
    };

    var obj = {
        usuario1: 1,
        usuario2: 2
    };

    $.ajax({
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem("token")
        },
        contentType: "application/json",
        url: "api/Conexoes/ConectarSeAUmPai_v1",
        method: "POST",
        data: JSON.stringify(obj),
        success: function (dados) {
            $("#txtRequest").html(JSON.stringify(request, undefined, 4));
            $("#txtResponse").html(JSON.stringify(dados, undefined, 4))
        }
    });
}
