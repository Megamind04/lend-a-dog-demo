$(document).ready(function () {
    "use strict";

    var inputBtn = $(".modal-footer input");
    var responsDiv = $("#dynamicDiv");

    var oldContent = $(
        "<label for='message-text' class='form-control-label'>Message:</label>" +
        "<textarea class='form-control' id='message-text' name='Message'></textarea>"
    );
    var errorMessage = $(
        "<div class='container'>" +
        "<h1 style='color:red'>Something went wrong!!!</h1>" +
        "</div>"
    );
    var successMessage = $(
        "<div class='container'>" +
        "<h1 style='color:green'>Message sent successfully!!!</h1>" +
        "</div>"
    );

    var message;

    $("#exampleModal").unbind().on("show.bs.modal", function (event) {
        var button = $(event.relatedTarget); // Button that triggered the modal

        var recipientFullName = button.data('whatever1');
        var recipientID = button.data('whatever2');

        var modal = $(this);

        modal.find(".modal-title").text("New message to " + recipientFullName);
        modal.find(".appendString").text(recipientFullName);
        modal.find(".hiddenID").val(recipientID);

        $("#valError").hide();
        inputBtn.show();
        responsDiv.empty();
        responsDiv.append(oldContent);
        $("#dynamicDiv textarea").val("");
    });

    $("#myForm").on("submit", function (e) {
        e.preventDefault();

        if (!$.trim($("#dynamicDiv textarea").val())) {
            $("#valError").show();
            return;
        }

        var dataToPost = $(this).serialize();

        $.post('/PersonalDashboard/AnswerToRequest', dataToPost)
            .done(function () {
                message = successMessage;
            })
            .fail(function (xhr) {

                if (xhr.status == 403) {

                    window.location = xhr.responseJSON.LogOnUrl;
                }

            }).always(function () {
                $("#valError").hide();
                inputBtn.hide();
                responsDiv.empty();
                responsDiv.append(message);
            });
    });

});