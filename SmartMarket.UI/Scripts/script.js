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