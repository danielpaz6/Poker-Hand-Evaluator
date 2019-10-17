$(function () {
    function loadData() {
        $.post("/Home/LoadScenario", function (data) {
            $("#data-place").html(data);
            $("#loading-text").hide();
            $("#eva-text").show();
            //alert(data);
            setTimeout(function () {
                $(".table").addClass("active");
                $("#eva-text").hide();
                $("#shuffle-cards").show();
            }, 1500);
        });
    }

    $("#shuffle-cards").click(function () {
        $(".table").removeClass("active");
        $("#shuffle-cards").hide();

        setTimeout(function () {
            //$("#loading-text").show();
            //$("#eva-text").hide();

            loadData();
        }, 500);
    });

    loadData();

    $('[data-toggle="tooltip"]').tooltip();
})