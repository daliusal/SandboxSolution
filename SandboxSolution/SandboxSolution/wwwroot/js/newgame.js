function onClickSubmit() {
    if (document.forms["new-game-form"].elements["game-name"].value == "") {
        alert("Game name cannot be empty");
        return;
    }
    postData("https://localhost:7077/api/Game");
}

async function onLoadDocument() {
    const json = await getData('https://localhost:7077/api/Publisher');
    console.log(json);
    loadPublishers(json);
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
    let data = {
        name: document.forms["new-game-form"].elements["game-name"].value,
        description: document.forms["new-game-form"].elements["game-description"].value,
        publisherId: document.forms["new-game-form"].elements["publisher"].value,
        quantity: document.forms["new-game-form"].elements["quantity"].value,
        price: document.forms["new-game-form"].elements["price"].value
    };

    const response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    }).then(function (response) {
        return response.text().then(function (text) {
            console.log(text);
            alert("wait");
        })
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
document.onLoad = onLoadDocument();