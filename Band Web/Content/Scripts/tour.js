//check login
$(".btn.btn-secondary").click(function (event) {
    var signUp = document.getElementById("signUp").innerText;
    if (signUp == "Become Our Fans!") {
        event.preventDefault();
        Swal.fire({
            title: "Don't have a account?",
            icon: 'warning',
            html: 'Why not <a href="#" class="alert-link">JOIN OUR FAMILY!</a><br/>Take some minutes and enjoy our show!',
            confirmButtonText: 'JOIN <i class="far fa-hand-point-left"></i>',
            background: '#BCBDBE',
            allowOutsideClick: false,
            showCancelButton: true,
            cancelButtonText: 'Next time...'
        }).then((result => {
            if (result.value) {
                window.location.href = "http://google.com";
            }
        }));
    }
});