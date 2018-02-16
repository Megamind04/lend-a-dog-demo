$(document).ready(function () {
    "use strict";

    var mainContainerDogsToDisplay = $(".carousel-inner");

    $.getJSON('/PersonalDashboard/JsonDogsPerUser')
        .done(function (data) {

            mainContainerDogsToDisplay.append(HtmlDisplayDogs(data));
        })
        .fail(function (xhr) {
            if (xhr.status === 403) {
                window.location = xhr.responseJSON.LogOnUrl;
            }
        });

    function HtmlDisplayDogs(data) {
        var stringHTML;

        $.each(data, (index, itemList) => {
            if (index === 0) {
                stringHTML = '<div class="carousel-item active">';
            }
            else {
                stringHTML = stringHTML + '<div class="carousel-item">';
            }

            stringHTML = stringHTML + '<div class="container">' + '<div class="card-deck ">';

            $.each(itemList, (index, item) => {
                //var showDogPhoto = '@Url.Action("ShowPhoto","PersonalDashboard")?dogId=' + item.DogID;
                var dogSize = GetDogSize(item.DogSize);
                stringHTML = stringHTML + '<div class="card">' +
                    '<img class="card-img-top" src="data:image/jpeg;base64,' + item.LastDogPhoto +'" alt= "Card image cap" > ' +
                    '<div class="card-block">' +
                    '<h4 class="card-title">Dog Info:</h4>' +
                    '<p class="card-text">Name: ' + item.DogName + '</p>' +
                    '<p class="card-text">Size: ' + dogSize + '</p>' +
                    '<div class="card-footer">' +
                    '<small class="text-muted">Last updated 3 mins ago</small>' +
                    '</div>' +
                    '</div>' +
                    '</div>';
            });

            stringHTML = stringHTML +
                '</div>' +
                '</div>' +
                '</div>';
        });
        return stringHTML;
    }

    function GetDogSize(enumSize) {
        if (enumSize == 1) {
            return 'Small';
        } else if (enumSize == 2) {
            return 'Average'
        } else if (enumSize == 3) {
            return 'Large';
        }
    };
});