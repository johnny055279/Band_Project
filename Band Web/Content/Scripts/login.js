$('#login-button').click(function () {
    $('#login-button').fadeOut("slow", function () {
        $("#container").fadeIn();
        TweenMax.from("#container", .4, { scale: 0, ease: Sine.easeInOut });
        TweenMax.to("#container", .4, { scale: 1, ease: Sine.easeInOut });
    });
});

$(".close-btn").click(function () {
    TweenMax.from("#container", .4, { scale: 1, ease: Sine.easeInOut });
    TweenMax.to("#container", .4, { left: "0px", scale: 0, ease: Sine.easeInOut });
    $("#container, #forgotten-container").fadeOut(800, function () {
        $("#login-button").fadeIn(800);
    });
});

$('#forgotten').click(function () {
    $("#container").fadeOut(function () {
        $("#forgotten-container").fadeIn();
    });
});

$('#send-email').click(function () {
    $.ajax({
        url: "/loginPage/getEmail",
        type: "POST",
        data: { email: $("#email").val() },
        success: function (data) {
            if (data == 1) {
                $("#email").val("");
                Swal.fire({
                    position: 'center',
                    type: 'success',
                    title: 'Email has been send!',
                    showConfirmButton: false,
                    timer: 1500
                });
            }
            else {
                $("#email").val("");
                Swal.fire({
                    type: 'error',
                    title: 'Oh my god...',
                    text: 'Fail to send email',
                    footer: 'Did you forget your email too?'
                })
            }
        },
        error: function () {
            $("#email").val("");
            Swal.fire({
                type: 'error',
                title: 'Oops...',
                text: 'Something went wrong!',
                footer: '<a href="#">Why do I have this issue?</a>'
            });
        }
    });
});