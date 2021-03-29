// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//const editSubmitBtn = document.getElementById('editSubmitBtn');
//editSubmitBtn.addEventListener('onClick', alert("Björn e bäst"));

//add a new style 'foo'
$.notify.addStyle('foo', {
    html:
        "<div>" +
        "<div class='clearfix'>" +
        "<div class='title' data-notify-html='title'/>" +
        "</div>" +
        "</div>" +
        "</div>"
});

const urlParams = new URLSearchParams(window.location.search);
const notification = urlParams.get('notify');

if (notification == "checkout") {
    $.notify({
        title: 'Vehicle successfully checked out.',
    }, {
        style: 'foo',
        autoHide: true,
        clickToHide: true
    });
}

if (notification == "edit") {
    $.notify({
        title: 'The vehicle was successfully edited.',
    }, {
        style: 'foo',
        autoHide: true,
        clickToHide: true
    });
}

if (notification == "parked") {
    $.notify({
        title: 'The vehicle was successfully parked.',
    }, {
        style: 'foo',
        autoHide: true,
        clickToHide: true
    });
}

$(document).ready(function () {
    $('[data-toggle="popover"]').popover();
});