const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const apiBase = "https://localhost:7077/api";
var game;

function onClickEdit() {
    if (document.forms["edit-game-form"].elements["game-name"].value == "") {
        alert("Game name cannot be empty");
        return;
    }
    postData(apiBase + "/Game");
}

async function onLoadDocument() {
    const publishersArray = await getData(apiBase + "/Publisher");
    loadPublishers(publishersArray);
    game = await getData(apiBase + "/Game/" + urlParams.get("id"));
    loadGameData(game);
}

async function getData(url) {
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

async function postData(url) {
    console.log(game.id);
    let data = {
        id: game.id,
        name: document.forms["edit-game-form"].elements["game-name"].value,
        description: document.forms["edit-game-form"].elements["game-description"].value,
        publisherId: document.forms["edit-game-form"].elements["publisher"].value,
        isOutOfStock: game.isOutOfStock,
        quantity: document.forms["edit-game-form"].elements["quantity"].value,
        price: document.forms["edit-game-form"].elements["price"].value,
        isDeleted: document.forms["edit-game-form"].elements["deleted"].checked
    };

    const response = await fetch(url, {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    }).then(function () {
        location.href = "/admin/games";
    });
}

function loadPublishers(publishers) {
    var select = document.getElementById("publisher");
    for (let publisher of publishers) {
        let option = document.createElement("option");
        option.value = publisher.id;
        option.innerHTML = publisher.name;
        select.appendChild(option);
    }
}

function loadGameData() {
    document.forms["edit-game-form"].elements["game-name"].value = game.name
    document.forms["edit-game-form"].elements["game-description"].value = game.description
    document.forms["edit-game-form"].elements["publisher"].value = game.publisherId;
    document.forms["edit-game-form"].elements["quantity"].value = game.quantity;
    document.forms["edit-game-form"].elements["price"].value = game.price;
    document.forms["edit-game-form"].elements["deleted"].checked = game.isDeleted;
}

function onClickReset() {
    loadGameData();
}

document.onLoad = onLoadDocument();