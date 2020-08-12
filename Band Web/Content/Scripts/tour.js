$(".btn.btn-secondary").click(function () {
    var signUp = document.getElementById("signUp").innerText;
    if (signUp == "Become Our Fans!") {
        Swal.fire(
            'Good job!',
            'You clicked the button!',
            'success'
        )
    }
});