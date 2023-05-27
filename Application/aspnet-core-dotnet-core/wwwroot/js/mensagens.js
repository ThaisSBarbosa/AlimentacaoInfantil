function enviarMensagem() {

    var obj = {
        conteudo: $("#Conteudo").val(),
        assunto: $("#Assunto").val(),
        remetente: $("#nomeRemetente").val(),
        destinatario: $("#nomeDestinatario").val()
    };

    $.ajax({
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem("token")
        },
        contentType: "application/json",
        url: "../api/MensagensAPI/EnviarMensagem_v1",
        method: "POST",
        data: JSON.stringify(obj),
        success: function (dados) {
            Swal.fire({
                position: 'top-center',
                icon: 'success',
                title: 'Mensagem enviada! Aguarde o contato do nutricionista.',
                showConfirmButton: false,
                timer: 1200
            });
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