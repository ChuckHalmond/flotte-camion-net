var ajaxUnobstrusiveValidation = function (result) {
    $.validator.unobtrusive.parse($(result));
};

$(document).ready(function () {
    remoteSubmit = $("#remote-submit");
    submit = $("#submit");

    if (remoteSubmit !== null) {
        remoteSubmit.on("click", function () {
            if (remoteSubmit !== null) {
                submit.click();
            }
        });
    }
});