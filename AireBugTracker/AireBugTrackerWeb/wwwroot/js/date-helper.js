
$(function () {
    $(".date").each(function (elem) {
        var utcDate = $(this).html();
        var date = new Date(utcDate);
        $(this).html(date.toLocaleString());
    })
});
