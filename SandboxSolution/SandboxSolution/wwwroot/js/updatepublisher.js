const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const apiBase = "https://localhost:7077/api";
var publisher;

function onClickEdit() {
    if (document.forms["edit-publisher-form"].elements["publisher-name"].value == "") {
        alert("Publisher name cannot be empty");
        return;
    }
    postData(apiBase + "/Publisher");
}

async function onLoadDocument() {
    publisher = await getData(apiBase + "/Publisher/" + urlParams.get("id"));
    loadGameData(publisher);
}

async function getData(url) {
    console.log(url);
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
        id: publisher.id,
        name: document.forms["edit-publisher-form"].elements["publisher-name"].value,
        isDeleted: document.forms["edit-publisher-form"].elements["deleted"].checked
    };

    const response = await fetch(url, {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    }).then(function () {
        location.href = "/admin/publishers";
    });
}

function loadGameData() {
    document.forms["edit-publisher-form"].elements["publisher-name"].value = publisher.name
    document.forms["edit-publisher-form"].elements["deleted"].checked = publisher.isDeleted;
}

function onClickReset() {
    loadGameData();
}

document.onLoad = onLoadDocument();