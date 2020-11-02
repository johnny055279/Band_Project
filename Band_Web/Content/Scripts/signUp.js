$(document).ready(function () {
    //get countries
    $.getJSON("/Content/ExternalFile/Countries.json", "", function (countries) {
        $.each(countries, function (i, item) {
            $("#inputCountry").append(`<option>${item.name}</option>`);
        });
    });
});