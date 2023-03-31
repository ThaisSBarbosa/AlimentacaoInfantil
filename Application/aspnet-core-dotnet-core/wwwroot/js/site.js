﻿$(function () {
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

function exibirPosts() {
    var request = {
        url: "Posts/ExibirPosts",
        type: "get",
        dataType: "json"
    };
    $.get("Posts/ExibirPosts", function (dados) {
        $("#txtRequest").html(JSON.stringify(n, undefined, 4));
        $("#txtResponse").html(JSON.stringify(t, undefined, 4))
    }, "json")
}

function fazerPost() {
    var request = {
        url: "Posts/FazerPost",
        type: "post",
        data: '{ conteudo: "Post teste", autor: 2, amei: 0, anuncio: false }',
        dataType: "json"
    };
    $.post("Posts/FazerPost", {
        conteudo: "Post teste",
        autor: 2,
        amei: 0,
        anuncio: false
    }, function (dados) {
        $("#txtRequest").html(JSON.stringify(n, undefined, 4));
        $("#txtResponse").html(JSON.stringify(t, undefined, 4))
    }, "json")
}

function editarPost() {
    var request = {
        url: "Posts/EditarPost",
        type: "put",
        data: '{ codigo: [o primeiro registro do banco], conteudo: "novo conteúdo" }',
        dataType: "json"
    };
    $.ajax({
        url: "Posts/EditarPost",
        method: "PUT",
        data: {
            codigo: 1,
            conteudo: "novo conteúdo"
        },
        success: function (dados) {
            $("#txtRequest").html(JSON.stringify(n, undefined, 4));
            $("#txtResponse").html(JSON.stringify(t, undefined, 4))
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
    $.ajax({
        url: "Posts/ApagarPost",
        method: "DELETE",
        data: {
            codigo: 1
        },
        success: function (dados) {
            $("#txtRequest").html(JSON.stringify(n, undefined, 4));
            $("#txtResponse").html(JSON.stringify(t, undefined, 4))
        }
    })
}

function exibirAnuncios() { }

function fazerAnuncio() { }

function editarAnuncio() { }

function apagarAnuncio() { }

function enviarAmei() { }

function retirarAmei() { }

function enviarMensagem() { }

function responderMensagem() { }

function conectarSePaiMae() {
    var request = {
        url: "Conexoes/ConectarSeAUmPai",
        type: "post",
        data: "{usuario1: [usuario criado para este teste], usuario2: [usuario criado para este teste]}",
        dataType: "json"
    };
    $.post("Conexoes/ConectarSeAUmPai", {
        usuario1: 1,
        usuario2: 2
    }, function (dados) {
        $("#txtRequest").html(JSON.stringify(request, undefined, 4));
        $("#txtResponse").html(JSON.stringify(dados, undefined, 4))
    }, "json")
}
