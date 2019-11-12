function ToggleMenu()
{
    var menu = document.getElementById("compact-menu");

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
    }
    else
    {
        image.style.maxHeight = "none";
        image.style.maxWidth = "none";
    }
}