function fazerNovoPost() {

    var obj = {
        conteudo: $("#conteudo").val(),
        autor: 6,
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
        }
    });
}


