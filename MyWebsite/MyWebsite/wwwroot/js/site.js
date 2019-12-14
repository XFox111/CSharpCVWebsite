const delay = ms => new Promise(res => setTimeout(res, ms));
function ToggleMenu()
{
    var menu = document.getElementsByClassName("main-menu")[0];

    if (menu.style.display == "none")
        menu.style.display = "initial";
    else
        menu.style.display = "none";
}

function ToggleImageSize()
{
    var image = document.getElementById("image");

    if (image.style.maxHeight == "none")
    {
        image.style.maxHeight = "50vh";
        image.style.maxWidth = "100%";
        image.style.cursor = "zoom-in";
    }
    else
    {
        image.style.maxHeight = "none";
        image.style.maxWidth = "none";
        image.style.cursor = "zoom-out";
    }
}