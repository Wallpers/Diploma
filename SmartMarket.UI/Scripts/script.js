$(document).ready(function ()
{
    delayClear();
});

function delayClear()
{
    setTimeout(hideStatus, 3000);
};

function hideStatus()
{
    $(".status-message").css("display", "none");
};

function openNav()
{
    $(".sidenav").toggleClass("hide-menu");

    $(".menu-top").toggleClass("menu-top-click");
    $(".menu-middle").toggleClass("menu-middle-click");
    $(".menu-bottom").toggleClass("menu-bottom-click");
};


function moveDiv() {
    $("#description-block").toggleClass("half-right");

    $("#slider").toggleClass("slider-left");
    $("#slider").toggleClass("slider-right");
};

var detachModal = $("#detach-modal");
var attachModal = $("#attach-modal");

var attachButton = $("#attach-balance-icon");
var detachButton = $(".detach-balance-icon");

var closeAttach = $("#close-attach-balance");
var closeDetach = $("#close-detach-balance");

attachButton.click(function () {
    attachModal.css("display", "block");
});

detachButton.click(function () {
    detachModal.css("display", "block");
});

// Because span addin dynamicaly, just click() doesn't work.
$(document).on('click', '#close-attach-balance', function ()
{
    attachModal.css("display", "none");
});

$(document).on('click', '#close-detach-balance', function ()
{
    detachModal.css("display", "none");
});

//window.onclick = function (event) {
//    if (event.target == modal) {
//        detachModal.css("display", "none");
//        attachModal.css("display", "none");
//    }
//}

$(":input").inputmask();