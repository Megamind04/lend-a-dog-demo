var mainContainer;
var info;       
var modalSize = $('#modalSize');
var inputBtn = $('.modal-footer input');
var mainContainerDogs = $(".carousel-inner");
var mainContainerConversations = $('#scrollDiv1');
var mainContainerConfirmations = $('#scrollDiv2');
var formHeader = $('.modal-header');
var formBody = $('.modal-body');

var getInfo = {
    'DogDelete': function (Id) {
        return ['/Dog/DeleteDogDisplay?DogID=' + Id, '<h2 class="modal-title">Delete Dog :</h2>',
            '/PersonalDashboard/DogsPerUser', '/Dog/DeleteDog', 'Small'];
    },
    'DogEdit': function (Id) {
        return ['/Dog/EditDogDisplay?DogID=' + Id, '<h2 class="modal-title">Edit Dog :</h2>',
            '/PersonalDashboard/DogsPerUser', '/Dog/EditDog', 'Small'];
    },
    'CreateDog': function (Id) {
        return ['/Dog/AddNewDog', '<h2 class="modal-title">Create New Dog :</h2>',
            '/PersonalDashboard/DogsPerUser', '/Dog/AddNewDog', 'Large'];
    },
    'Approve': function (Id) {
        return ['/DogOwner/VerifyDogOwnerDisplay?UserID=' + Id, '<h2 class="modal-title">Verify User :</h2>',
            '/PersonalDashboard/ConfirmationsPerUser', '/DogOwner/VerifyDogOwner', 'Small'];
    },
    'Deny': function (Id) {
        return ['/DogOwner/DenyDogOwnerDisplay?RequestFromID=' + Id, '<h2 class="modal-title">Delete Request :</h2>',
            '/PersonalDashboard/ConfirmationsPerUser', '/DogOwner/DenyDogOwner', 'Small'];
    },
    'Conversation': function (Id) {
        return ['/DogOwner/OpenConversation?OtherId=' + Id, '<h2 class="modal-title">Conversations :</h2>',
            '/PersonalDashboard/ConversationsPerUser', '/DogOwner/SubmitConversation', 'Scroll'];
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
    }
}

function getHtml(url, callback) {

    $.get(url)
        .done(function (data) {
            callback(data);
        })
        .fail(function () {
            modalSize.removeClass('modal-lg');
            callback('<h1>Something went wrong!!!</h1>');
        });
}

function getReload(url, callback) {

    modalSize.removeClass('modal-lg');
    formBody.empty();
    formBody.append('<h1>Success!!!</h1>');
    inputBtn.hide();

    $.get(url)
        .done(function (data) {

            mainContainer.html(data);

        })
        .fail(function (xhr) {
            if (xhr.status === 403) {
                window.location = xhr.responseJSON.LogOnUrl;
            }
        });
    callback(url);
}