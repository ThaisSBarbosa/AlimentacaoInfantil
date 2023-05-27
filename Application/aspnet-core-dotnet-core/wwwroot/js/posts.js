function fazerNovoPost() {

    var obj = {
        conteudo: $("#conteudo").val(),
        autor: sessionStorage.getItem("id"),
        amei: 0,
        anuncio: false
    };

    $.ajax({
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem("token")
        },
        contentType: "application/json",
        url: "../api/PostsAPI/FazerPost_v1",
        method: "POST",
        data: JSON.stringify(obj),
        success: function (dados) {
            Swal.fire({
                position: 'top-center',
                icon: 'success',
                title: 'Post realizado!',
                showConfirmButton: false,
                timer: 1200
            });
            $("#conteudo").val('');
            window.location = "/posts/index";
        },
        error: function (dados) {
            Swal.fire({
                position: 'top-center',
                icon: 'error',
                title: 'Erro!',
                showConfirmButton: false,
                timer: 1200
            });
        }
    });
}

function amei(codigo) {

    var obj = {
        codigo: codigo
    };

    $.ajax({
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem("token")
        },
        contentType: "application/json",
        url: "../api/PostsAPI/EnviarAmei_v1",
        method: "PUT",
        data: JSON.stringify(obj),
        success: function (dados) {
            Swal.fire({
                position: 'top-center',
                icon: 'success',
                title: 'Amei enviado!',
                showConfirmButton: false,
                timer: 1200
            });
            window.location = "/posts/index";
        },
        error: function (dados) {
            Swal.fire({
                position: 'top-center',
                icon: 'error',
                title: 'Erro!',
                showConfirmButton: false,
                timer: 1200
            });
        }
    })
}


