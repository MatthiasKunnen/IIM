$(function() {
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
});