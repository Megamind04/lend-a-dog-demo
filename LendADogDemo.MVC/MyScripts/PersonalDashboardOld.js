$(document).ready(function () {

    var inputBtn = $('.modal-footer input');
    var mainContainerDogs = $('.carousel-inner');
    var mainContainerConversations = $('.scrollDiv1');
    var mainContainerConfirmations = $('.scrollDiv2');

    $('#personalDashboardModal').unbind().on('show.bs.modal', function (event) {

        var button = $(event.relatedTarget);

        var modal = $(this);

        inputBtn.show();

        var forminput = modal.find('.modal-body');
        forminput.empty();

        getHtml(button, forminput);
    });

    function getHtml(button, forminput) {

        var url;

        if (button.attr('id') === 'DogDelete') {
            var Id = button.data('id');
            url = '/Dog/DeleteDogDisplay?DogID=' + Id;
        }
        else if (button.attr('id') === 'DogEdit') {
            var Id = button.data('id');
            url = '/Dog/EditDogDisplay?DogID=' + Id;
        }
        else if (button.attr('id') === 'CreateDog') {
            url = '/Dog/AddNewDog';
        }
        else if (button.attr('id') === 'Approve') {
            var Id = button.data('id');
            url = '/DogOwner/VerifyDogOwnerDisplay?UserID=' + Id;
        }
        //else if

        $.get(url)
            .done(function (data) {
                forminput.html(data);
                $.validator.unobtrusive.parse("#personaldashboardform");
            })
            .fail(function () {
                forminput.html('<h1 id="Msg" >Something went wrong!!!</h1>')
            });
    };

    $('#personaldashboardform').on('submit', function (e) {

        e.preventDefault();

        var inputid = $('.form-group:first');

        var urls = getUrl(inputid);
        var urlPost = urls[0];
        var urlGet = urls[1];
        if (urlPost === '/Dog/AddNewDog') {

            var formData = new FormData(this);

            $.ajax({
                type: 'POST',
                enctype: 'multipart/form-data',
                url: urlPost,
                data: formData,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    $('#Msg').text('Success!!!');
                    getReload(urlGet);
                    inputBtn.hide();
                },
                error: function (data) {
                    if (xhr.status == 403) {

                        window.location = xhr.responseJSON.LogOnUrl;
                    }
                    $('#Msg').text('Something went wrong!!!');
                }
            });


        } else {
            var dataToPost = $(this).serialize();
            $.post(urlPost, dataToPost)
                .done(function () {
                    $('#Msg').text('Success!!!');
                    getReload(urlGet);
                    inputBtn.hide();
                })
                .fail(function (xhr) {
                    if (xhr.status == 403) {

                        window.location = xhr.responseJSON.LogOnUrl;
                    }
                    $('#Msg').text('Something went wrong!!!');
                });
        }
    });

    function getUrl(inputid) {
        if (inputid.attr('id') === 'DeleteDogPost') {
            var urls = ['/Dog/DeleteDog', '/PersonalDashboard/DogsPerUser'];
            return urls;
        }
        else if (inputid.attr('id') === 'EditDogPost') {
            var urls = ['/Dog/EditDog', '/PersonalDashboard/DogsPerUser'];
            return urls;
        }
        else if (inputid.attr('id') === 'CreateDogPost') {
            var urls = ['/Dog/AddNewDog', '/PersonalDashboard/DogsPerUser'];
            return urls;
        }
    };

    function getReload(urlGet) {

        var mainContainer;
        var url;
        if (urlGet === '/PersonalDashboard/DogsPerUser') {
            mainContainer = mainContainerDogs;
            url = urlGet;
        };
        $('#personalDashboardModal').on('hide.bs.modal', function (e) {
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
        })
    };
});