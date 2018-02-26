$(document).ready(function () {

    'use strict';

    $('#personalDashboardModal').unbind().on('show.bs.modal', function (event) {

        var button = $(event.relatedTarget);
        var modal = $(this);

        inputBtn.show();

        var btnId = button.attr('id');
        var Id = button.data('id');

        info = getInfo[btnId](Id);
        if (info[4] === 'Large' || info[4] === 'Scroll') {
            modalSize.addClass('modal-lg');
        }

        getHtml(info[0], function (data) {
            formHeader.append(info[1]);
            formBody.append(data);
            $.validator.unobtrusive.parse("#personaldashboardform");
        });
    });

    $('#personalDashboardModal').on('shown.bs.modal', function (event) {
        if (info[4] === 'Scroll') {
            $("#scrollDiv3").animate({
                scrollTop: $('#scrollDiv3')[0].scrollHeight - $('#scrollDiv3')[0].clientHeight
            }, 1000);
            inputBtn.hide();
        }
    });

    $('#personaldashboardform').on('submit', function (e) {

        e.preventDefault();

        var dataToPost;
        $.ajax({
            type: 'POST',
            enctype: info[3] === '/Dog/AddNewDog' ? 'multipart/form-data' : null,
            url: info[3],
            data: info[3] === '/Dog/AddNewDog' ? dataToPost = new FormData(this) : dataToPost = $(this).serialize(),
            cache: false,
            contentType: info[3] === '/Dog/AddNewDog' ? false : 'application/x-www-form-urlencoded; charset=UTF-8',
            processData: info[3] === '/Dog/AddNewDog' ? false : true
        }).done(function (data) {
            if (info[4] === 'Scroll') {
                getHtml(info[0], function (htmlData) {
                    formBody.empty();
                    formBody.append(htmlData);
                    $("#scrollDiv3").animate({
                        scrollTop: $('#scrollDiv3')[0].scrollHeight - $('#scrollDiv3')[0].clientHeight
                    }, 1000);
                });
            } else {
                getReload(info[2], setContainer);
            }
        }).fail(function (xhr) {
            if (xhr.status === 403) {
                window.location = xhr.responseJSON.LogOnUrl;
            } else {
                formBody.empty();
                formBody.append('<h1>Something went wrong!!!</h1>');
                inputBtn.hide();
            }
        });
    });

    $('#personalDashboardModal').on('hide.bs.modal', function (e) {
        if (info[4] === 'Scroll') {
            setContainer(info[2]);
            getHtml(info[2], function (data) {
                mainContainer.html(data);
            });
        }
    });


    $('#personalDashboardModal').on('hidden.bs.modal', function (e) {
        modalSize.removeClass('modal-lg');
        formHeader.empty();
        formBody.empty();
        mainContainer = null;
        info = null;
    });
});