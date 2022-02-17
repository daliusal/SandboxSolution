const apiBase = "https://localhost:7077/api";
const oDataLogialOperators = {
    'Equal': 'eq',
    'Not equal': 'ne',
    'Greater than': 'gt',
    'Greater than or equal': 'ge',
    'Less than': 'lt',
    'Less than or equal': 'le'
}
const gameColumns = [
    'Name', 'Description', 'Quantity', 'Price', 'IsDeleted'
];
var filtersCount = 0;

async function onLoadDocument() {
    console.log("Making API call...");
    const json = await getData(apiBase + "/Game/getall?$orderby=Name");
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
            '<button type="button" class="btn btn-danger" onclick="onClickDelete(this)">Delete</button></td>';
        tr.innerHTML = td;
        tbody.appendChild(tr);
    }
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

async function deleteData(url) {
    const response = await fetch(url, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(function () {
        location.reload();
    });
}

function onClickEdit(button) {
    location.href = "/admin/games/edit?id=" + button.parentElement.parentElement.id;
}

function onClickAddNew() {
    location.href = "/admin/games/newgame";
}

function onClickDelete(button) {
    if (confirm("Are you sure you want to delete '" +
        button.parentElement.parentElement.firstChild.innerHTML + "'?")) {
        deleteData(apiBase + "/Game/" + button.parentElement.parentElement.id);
    }

}
function onClickAddFilter() {
    //TODO: Get filters element
    //TODO: Build filter object
    addFilter(oDataLogialOperators, gameColumns);
}

async function onClickFilter() {
    const query = buildQuery();
    document.getElementById("tbody").innerHTML = "";
    const json = await getData(apiBase + "/Game/getall?" + query);
    loadDataIntoTable(json);
}
document.onload = onLoadDocument();