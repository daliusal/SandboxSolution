async function onLoadDocument() {
    console.log("Making API call...");
    const json = await getData('https://localhost:7077/api/Game/getall');
    loadDataIntoTable(json);
}

function loadDataIntoTable(data) {
    let tbody = document.getElementById("tbody");
    for (let game of data) {
        let tr = document.createElement("tr");
        tr.id = game.id;
        let td;
        td = "<td>" + game.name + "</td><td>" + game.description + "</td><td>" +
            game.quantity + "</td><td>€" + game.price + "</td><td>" + game.isDeleted + "</td><td>" +
            '<button type="button" class="btn btn-primary" onclick="onClickEdit(this)">Edit</button> ' +
            '<button type="button" class="btn btn-danger">Delete</button></td>';
        tr.innerHTML = td;
        tbody.appendChild(tr);
    }
}

async function getData(url = '') {
    var json;
    const response = await fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(function (response) {
        return response.text().then(function (text) {
            console.log(text);
            //json = JSON.parse(text);
        })
    });

    return json;
}

function onClickEdit(button) {
    console.log(button.parentElement.parentElement.id);
}

function onClickAddNew() {
    location.href = "/admin/games/newgame";
}

document.onload = onLoadDocument();