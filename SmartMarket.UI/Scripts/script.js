function openNav()
{
    var sidenav = $("#sidenav");

    var top = $(".menu-top");
    var middle = $(".menu-middle");
    var bottom = $(".menu-bottom");


    if (sidenav.hasClass("sidenav show"))
    {
        sidenav.removeClass("show");

        top.removeClass("menu-top-click");
        middle.removeClass("menu-middle-click");
        bottom.removeClass("menu-bottom-click");
    }
    else
    {
        sidenav.addClass("show");

        top.addClass("menu-top-click");
        middle.addClass("menu-middle-click");
        bottom.addClass("menu-bottom-click");
    }
}