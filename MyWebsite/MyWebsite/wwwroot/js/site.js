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

function UpdateProjects()
{
    // Settings badges tooltips
    var badges = document.getElementsByClassName("badge");
    for (var i = 0; i < badges.length; i++)
    {
        switch (badges[i].classList[1])
        {
            case "csharp":
                badges[i].setAttribute("title", "C# Programming language");
                break;
            case "dotnet":
                badges[i].setAttribute("title", ".NET Framework");
                break;
            case "xamarin":
                badges[i].setAttribute("title", "Xamarin Framework");
                break;
            case "unity":
                badges[i].setAttribute("title", "Unity Engine");
                break;
            case "uwp":
                badges[i].setAttribute("title", "Universal Windows Platform");
                break;
            case "windows":
                badges[i].setAttribute("title", "Windows Platform");
                break;
            case "win32":
                badges[i].setAttribute("title", "Windows Platform (Win32)");
                break;
            case "android":
                badges[i].setAttribute("title", "Android Platform");
                break;
        }
    }

    // Making projects descriptions multiline
    var descriptions = document.getElementsByClassName("description");
    for (var i = 0; i < descriptions.length; i++)
    {
        var desc = descriptions[i];
        var text = desc.innerText;
        desc.innerText = "";
        desc.innerHTML = text;
    }
}

function PrintCV() {
    var printFrame = window.open("", "", "height=500, width=800");

    printFrame.document.write("<html>");
    printFrame.document.write(document.head.outerHTML);
    printFrame.document.write("<body>");
    printFrame.document.write(document.getElementById("cv").outerHTML);
    printFrame.document.write("</body>");
    printFrame.document.write("</html>");

    printFrame.document.close();
    setTimeout(function () {
        printFrame.print();
        printFrame.close();
    }, 500);
}