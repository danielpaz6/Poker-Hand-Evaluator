$(function () {
    setTimeout(function () {
        $(".table").addClass("active");
        $("#eva-text").hide();
    }, 1500);

    $('[data-toggle="tooltip"]').tooltip();
})