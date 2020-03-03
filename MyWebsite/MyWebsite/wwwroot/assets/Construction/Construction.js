async function Load()
{
    var output = document.getElementById("output");

    await delay(500);
    output.children[0].style.display = 'block';
    document.getElementById('status').innerText = "Build started...";
    document.title = "Build started... - XFox111";

    await delay(3000);
    output.children[1].style.display = 'block';

    await delay(1000);
    document.getElementById('status').innerText = "Deploy started...";
    document.title = "Deploy started... - XFox111";
    output.children[2].style.display = 'block';

    await delay(3000);
    output.children[3].style.display = 'block';
    await delay(5);
    output.children[4].style.display = 'block';
    await delay(5);
    output.children[5].style.display = 'block';
    await delay(5);
    output.children[6].style.display = 'block';

    await delay(2000);
    document.getElementById('status').innerText = "Deploy failed.";
    document.title = "Deploy failed... - XFox111";
    output.children[8].style.display = 'block';
    await delay(5);
    output.children[9].style.display = 'block';
    await delay(5);
    output.children[10].style.display = 'block';
    await delay(5);
    output.children[12].style.display = 'block';
    await delay(5);
    output.children[14].style.display = 'block';
    await delay(5);
    output.children[15].style.display = 'block';

    await delay(200);
    var k = 17;
    for (; k < output.children.length - 3; k++)
    {
        output.children[k].style.display = 'block';
        await delay(5);
    }

    await delay(2000);
    output.children[++k].style.display = 'block';
    await delay(5);
    output.children[++k].style.display = 'block';
    document.title = "Site is under construction - XFox111";
    document.getElementById('status').innerText = "Site is under construction";
}

const delay = ms => new Promise(res => setTimeout(res, ms));