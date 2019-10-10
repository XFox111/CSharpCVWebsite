function ToggleImageSize() {
    var image = document.getElementById("image");

    if (image.style.maxHeight == "none")
        image.style.maxHeight = "50vh";
    else
        image.style.maxHeight = "none";
}