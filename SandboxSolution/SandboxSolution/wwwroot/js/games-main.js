const apiBase = "https://localhost:7077/";
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

var pageQuery = "";
var query = "";

async function onLoadDocument() {
    let maxPages = await getData(apiBase + "odata/Game/$count?" + query);
    setCurrentPage(1);
    setMaxPages(maxPages);
    onClickPageSelection(document.getElementById("page-1"));
}

function loadDataIntoTable(data) {
    let tbody = document.getElementById("tbody");
    for (let game of data) {
        let tr = document.createElement("tr");
        tr.id = game.Id;
        let td;
        td = "<td>" + game.Name + "</td><td>" + game.Description + "</td><td>" +
            game.Quantity + "</td><td>€" + game.Price + "</td>";
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

function onClickAddFilter() {
    //TODO: Get filters element
    //TODO: Build filter object
    addFilter(oDataLogialOperators, gameColumns);
}

function onClickAddOrderBy() {
    addOrderBy(gameColumns);
}

async function onClickPageSelection(page) {
    pageQuery = getPageQuery(page);
    await filterData();
}

async function onClickFilter() {
    await filterData();
}

async function filterData() {
    var query = buildQuery();
    if (document.getElementById("tbody") != null)
        document.getElementById("tbody").innerHTML = "";
    if (query == "") {
        query = pageQuery;
    }
    else {
        query += "&" + pageQuery;
    }
    setCurrentPage(1);
    setMaxPages(await getData(apiBase + "odata/Game/$count?" + query));
    onClickPage(document.getElementById("page-1"));
    const json = await getData(apiBase + "odata/Game?" + query)
    loadDataIntoTable(json.value);
}

document.onload = onLoadDocument();