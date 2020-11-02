var check = "false";

$("#cancel").click(function () {
    $(".modal-body").children("input").val("");
});

$("#close").click(function () {
    $(".modal-body").children("input").val("");
});

$("#save-change").click(function () {
    if ($(".modal-body input:nth-child(4)").val() != $(".modal-body input:nth-child(6)").val()) {
        alert("Password Not Match...");
    }
    else if (check == "false") {
        alert("Please enter your old password...");
    }
    else {
        $("#pswModal").modal("hide");
    }
});

$(".modal-body input:nth-child(2)").on("blur", function () {
    console.log("blur");
    $.ajax({
        url: "/user/checkPassword",
        type: "POST",
        data: { password: $(".modal-body input:nth-child(2)").val() },
        success: function (data) {
            if (data != 1) {
                alert("Wrong Password");
                $(".modal-body input:nth-child(2)").val("");
            }
            else {
                check = "true";
            }
        },
        error: function () {
            alert("Server Error...");
        }
    });
});