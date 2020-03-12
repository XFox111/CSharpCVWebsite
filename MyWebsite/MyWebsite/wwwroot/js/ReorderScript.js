function Up(btn)
{
	var item = btn.parentNode.parentNode;
	var table = document.querySelector("tbody");

	table.insertBefore(item, item.previousElementSibling);

	document.querySelector("button").disabled = false;
}

function Down(btn)
{
	var item = btn.parentNode.parentNode;
	var table = document.querySelector("tbody");

	table.insertBefore(item, item.nextElementSibling.nextElementSibling);

	document.querySelector("button").disabled = false;
}

function ApplyReorder()
{
	var table = document.querySelector("tbody");

	var form = document.createElement("form");
	form.method = "post";
	form.hidden = true;

	document.body.appendChild(form);

	for (var k = 0; k < table.children.length; k++)
	{
		var item = document.createElement("input");
		item.type = "text";
		item.name = "reorderList";
		item.value = table.children[k].id;
		form.appendChild(item);
	}

	form.submit();
}