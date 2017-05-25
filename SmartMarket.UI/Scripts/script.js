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


var modal = $("#myModal");

var btn = $("#create-balance-icon");

var span = $(".close")[0];

btn.click(function () {
    modal.css("display", "block");
});

span.click(function () {
    modal.css("display", "none");
});

window.onclick = function (event) {
    if (event.target == modal) {
        modal.css("display", "none");
    }
}

$(":input").inputmask();