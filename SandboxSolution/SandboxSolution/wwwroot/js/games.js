async function onLoadDocument() {
    console.log("Making API call...");
    const myJson = await getData('https://localhost:7077/api/Game/getall');
    loadDataIntoTable(myJson);
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
            json = JSON.parse(text);
        })
    });
    return json;
}

function loadDataIntoTable(data) {
    let tbody = document.getElementById("tbody");
    for (let game of data) {
        let tr = document.createElement("tr");
        let td;
        td = "<td>" + game.name + "</td><td>" + game.description + "</td><td>" +
            game.quantity + "</td><td>€" + game.price + "</td><td>" + game.isDeleted + "</td>";
        tr.innerHTML = td;
        tbody.appendChild(tr);
    }
}

document.onload = onLoadDocument();