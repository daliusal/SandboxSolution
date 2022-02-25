const apiBase = "https://localhost:7077/";
const oDataLogialOperators = {
    'Equal': 'eq',
    'Not equal': 'ne',
    'Greater than': 'gt',
    'Greater than or equal': 'ge',
    'Less than': 'lt',
    'Less than or equal': 'le'
}
const publisherColumns = [
    'Name', 'IsDeleted'
];

var pageQuery = "";
var query = "";

async function onLoadDocument() {
    let maxPages = await getData(apiBase + "odata/publisher/$count?" + query);
    setCurrentPage(1);
    setMaxPages(maxPages);
    onClickPageSelection(document.getElementById("page-1"));
}

function loadDataIntoTable(data) {
    let tbody = document.getElementById("tbody");
    for (let publisher of data) {
        let tr = document.createElement("tr");
        tr.id = publisher.Id;
        let td;
        td = "<td>" + publisher.Name + "</td><td>" + publisher.IsDeleted + "</td><td>" +
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
    location.href = "/admin/publishers/edit?id=" + button.parentElement.parentElement.id;
}

function onClickAddNew() {
    location.href = "/admin/publishers/newpublisher";
}

function onClickDelete(button) {
    if (confirm("Are you sure you want to delete '" +
        button.parentElement.parentElement.firstChild.innerHTML + "'?")) {
        deleteData(apiBase + "api/Publisher/" + button.parentElement.parentElement.id);
    }
}
function onClickAddFilter() {
    //TODO: Get filters element
    //TODO: Build filter object
    addFilter(oDataLogialOperators, publisherColumns);
}

function onClickAddOrderBy() {
    addOrderBy(publisherColumns);
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
    setMaxPages(await getData(apiBase + "odata/Publisher/$count?" + query));
    onClickPage(document.getElementById("page-1"));
    const json = await getData(apiBase + "odata/Publisher?" + query)
    loadDataIntoTable(json.value);
}

document.onload = onLoadDocument();