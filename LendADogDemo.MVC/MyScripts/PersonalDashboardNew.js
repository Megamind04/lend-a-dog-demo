$(document).ready(function () {

    'use strict';

    var mainContainer;
    var inputBtn = $('.modal-footer input');
    var mainContainerDogs = $(".carousel-inner");
    var mainContainerConversations = $('.scrollDiv1');
    var mainContainerConfirmations = $('.scrollDiv2');
    var forminput = $('.modal-body');

    $('#personalDashboardModal').unbind().on('show.bs.modal', function (event) {

        var button = $(event.relatedTarget);
        var modal = $(this);

        inputBtn.show();
        mainContainer = null;
        forminput.empty();

        var btnId = button.attr('id');
        var Id = button.data('id');

        getHtml(getUrl[btnId](Id), function (data) {
            forminput.html(data);
            $.validator.unobtrusive.parse("#personaldashboardform");
        });
    });

    function getHtml(url, callback) {

        $.get(url)
            .done(function (data) {
                callback(data);
            })
            .fail(function () {
                callback('<h1 id="Msg" >Something went wrong!!!</h1>')
            });
    };

    $('#personaldashboardform').on('submit', function (e) {

        e.preventDefault();

        var input = $('.form-group:first');
        var inputId = input.attr('id');

        var urls = getUrl[inputId]();

        var urlPost = urls[0];
        var urlGet = urls[1];

        var dataToPost;
        $.ajax({
            type: 'POST',
            enctype: urlPost === '/Dog/AddNewDog' ? 'multipart/form-data' : null,
            url: urlPost,
            data: urlPost === '/Dog/AddNewDog' ? dataToPost = new FormData(this) : dataToPost = $(this).serialize(),
            cache: false,
            contentType: urlPost === '/Dog/AddNewDog' ? false : 'application/x-www-form-urlencoded; charset=UTF-8',
            processData: urlPost === '/Dog/AddNewDog' ? false : true,
        }).done(function (data) {

            getReload(urlGet, setContainer);

        }).fail(function (xhr) {

            if (xhr.status == 403) {

                window.location = xhr.responseJSON.LogOnUrl;
            } else {
                $('#Msg').text('Something went wrong!!!');
                inputBtn.hide();
            };
        });
    });

    function getReload(url, callback) {

        $('#Msg').text('Success!!!');
        inputBtn.hide();
        callback(url);

        $.get(url)
            .done(function (data) {
                mainContainer.html(data)
            })
            .fail(function (xhr) {
                if (xhr.status == 403) {

                    window.location = xhr.responseJSON.LogOnUrl;
                }
                $('#Msg').text('Something went wrong!!!');
            });
    };

    var getUrl = {
        'DogDelete': function (Id) {
            return '/Dog/DeleteDogDisplay?DogID=' + Id;
        },
        'DogEdit': function (Id) {
            return '/Dog/EditDogDisplay?DogID=' + Id;
        },
        'CreateDog': function (Id) {
            return '/Dog/AddNewDog';
        },
        'Approve': function (Id) {
            return '/DogOwner/VerifyDogOwnerDisplay?UserID=' + Id;
        },
        'DeleteDogPost': function (Id) {
            return ['/Dog/DeleteDog', '/PersonalDashboard/DogsPerUser'];
        },
        'EditDogPost': function (Id) {
            return ['/Dog/EditDog', '/PersonalDashboard/DogsPerUser'];
        },
        'CreateDogPost': function (Id) {
            return ['/Dog/AddNewDog', '/PersonalDashboard/DogsPerUser'];
        }
    };

    function setContainer(url) {
        switch (url) {
            case '/PersonalDashboard/DogsPerUser': mainContainer = mainContainerDogs;
                break;
            case '/PersonalDashboard/ConversationsPerUser': mainContainer = mainContainerConversations;
                break;
            case '/PersonalDashboard/ConfirmationsPerUser': mainContainer = mainContainerConfirmations;
                break;
            default: mainContainer = null;
        };
    };

    $('#personalDashboardModal').on('hidden.bs.modal', function (e) {
        forminput.empty();
    });
});