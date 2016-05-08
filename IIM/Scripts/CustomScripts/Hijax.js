$(function () {
    $("form.hijax").submit(function (e) {
        var hijaxForm = $(this);
        var hijaxDataId = hijaxForm.attr("data-hijax-id");
        if (hijaxDataId !== undefined) {
            $.post(hijaxForm.attr("action"),
                    hijaxForm.serialize(),
                    function (data) {
                        $(".hijax-result[data-hijax-id=\"" + hijaxDataId + "\"]").html(data);
                    });
            e.preventDefault();
        }
    });
    $(".hijax[data-hijax-trigger=\"click\"]").click(function (e) {
        var hijaxClickSource = $(this);
        e.preventDefault();

        $.post(hijaxClickSource.attr("data-hijax-action"),
                    hijaxClickSource.attr("data-hijax-data"),
                    function (data) {
                        switch (hijaxClickSource.attr("data-hijax-processing")) {
                            case "result":
                                $(".hijax-result[data-hijax-id=\"" + hijaxClickSource.attr("data-hijax-id") + "\"]").html(data);
                                break;
                            case "popup":
                                createPopup(data).appendTo($("body"));
                                break;
                        }
                    });
    });
});

function createPopup(body) {
    var closeButton = $("<i>").addClass("fa fa-remove popup-close");
    var popup = $("<div>").addClass("popup container").append(closeButton).append($("<div>").addClass("popup-body").html(body));
    closeButton.click(function() {
        popup.remove();
    });
    return popup;
}