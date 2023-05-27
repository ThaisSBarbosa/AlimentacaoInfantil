$(function () {

    preencheBemVindo();
});

function preencheBemVindo() {

    var nome = sessionStorage.getItem("nome");

    if (nome != null & nome != undefined)
        $("#btnBemVindo").text("Seja bem-vindo(a), " + nome + "!");
}

