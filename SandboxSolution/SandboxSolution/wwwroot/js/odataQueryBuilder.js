var filtersCount = 0;
var orderbyCount = 0;

var pages = [1, 2, 3]
var currentPage = pages[0];
var pageSize = 3;
var pageQuery = "";
var maxPage = 1;


function addFilter(operators, columns) {
    //TODO: create filter object containing multiple selects and value
    //      select = column, operator; value = text input
    //TODO: append all elements into div element
    //TODO: append div to filters container
    filtersCount++;
    let filtersContainer = document.getElementById("filters");
    let filter = document.createElement("div");
    filter.id = "filter-" + filtersCount;

    let columnSelect = document.createElement("select");
    columnSelect.id = "filter-column-" + filtersCount;
    for (const col of columns) {
        columnSelect.innerHTML += "<option value=" + col + ">" + col + "</option>";
    }

    let operatorsSelect = document.createElement("select");
    operatorsSelect.id = "filter-operator-" + filtersCount;
    for (const [key, value] of Object.entries(operators)) {
        operatorsSelect.innerHTML += "<option value=" + value + ">" + key + "</option>";
    }

    let value = document.createElement("input");
    value.id = "filter-value-" + filtersCount;

    filter.appendChild(columnSelect);
    filter.innerHTML += " ";
    filter.appendChild(operatorsSelect);
    filter.innerHTML += " ";
    filter.appendChild(value);

    filtersContainer.append(filter);
}

function addOrderBy(columns) {
    //TODO: create orderby object containing multiple selects
    //      select = column, order
    //TODO: append all elements into div element
    //TODO: append div to filters container
    orderbyCount++;
    let orderbyContainer = document.getElementById("orderby");
    let orderby = document.createElement("div");
    orderby.id = "orderby-" + orderbyCount;

    let columnSelect = document.createElement("select");
    columnSelect.id = "orderby-column-" + orderbyCount;
    for (const col of columns) {
        columnSelect.innerHTML += "<option value=" + col + ">" + col + "</option>";
    }
    let orderSelect = document.createElement("select");
    orderSelect.id = "orderby-order-" + orderbyCount;
    orderSelect.innerHTML = '<option value="desc">Desc</option><option value="asc">Asc</option>';

    orderby.appendChild(columnSelect);
    orderby.innerHTML += " ";
    orderby.appendChild(orderSelect);

    orderbyContainer.append(orderby);
}

function buildQuery() {
    //TODO: build query from filters and orderby elements
    //TODO: check if filters and orderby have any elements added
    //      if so build filter/orderby query and combine them
    let filter = "";
    let orderby = "";
    if(filtersCount > 0)
        filter = getFilter();
    if (orderbyCount > 0)
        orderby = getOrderBy();

    return orderbyCount > 0 && filtersCount > 0 ? filter + "&" + orderby : filter + orderby;
}

function getFilter() {
    //TODO: iterate thry all filters and append them to the filter string
    let filter = "$filter=";
    for (let i = 1; i <= filtersCount; i++) {

        let column = document.getElementById("filter-column-" + i).value;
        let operator = document.getElementById("filter-operator-" + i).value;
        let value = document.getElementById("filter-value-" + i).value;

        if (!isNaN(value))
            filter += column + " " + operator + " " + value;
        else
            filter += column + " " + operator + " '" + value + "'";
        if (i != filtersCount)
            filter += " and ";
    }

    return filter;
}

function getOrderBy() {
    //TODO: iterate thru all orderby filters and append to orderby filter
    let orderby = "$orderby=";
    for (let i = 1; i <= orderbyCount; i++) {

        let column = document.getElementById("orderby-column-" + i).value;
        let order = document.getElementById("orderby-order-" + i).value;

        orderby += column + " " + order;
        if (i != orderbyCount)
            orderby += ",";
    }

    return orderby;
}

function onClickPage(page) {
    document.getElementById("next").disabled = false;
    for (let i = 1; i <= pages.length; i++) {
        if (i > maxPage) {
            console.log(maxPage);
            document.getElementById("page-" + i).style.display = "none";
        }
        else {
            document.getElementById("page-" + i).style.display = "block";
        }
    }

    if (page == null)
        pageQuery = "$skip=" + (currentPage * pageSize - pageSize) + "&$top=" + pageSize;
    else {
        currentPage = parseInt(page.innerHTML);
        if (currentPage <= 1 || (maxPage == 2 && currentPage == 2)) {
            pages[0] = 1;
            pages[1] = 2;
            pages[2] = 3;
        }
        else if (currentPage >= maxPage) {
            pages[0] = maxPage - 2;
            pages[1] = maxPage - 1;
            pages[2] = maxPage;
        }
        else {
            pages[0] = currentPage - 1;
            pages[1] = currentPage;
            pages[2] = currentPage + 1;
        }        

        for (let i = 0; i < pages.length; i++) {
            document.getElementById("page-" + (i + 1)).innerHTML = pages[i];
        }
    }

    pageQuery = "$skip=" + (currentPage * pageSize - pageSize) + "&$top=" + pageSize;
}

function getPageQuery(page) {
    onClickPage(page);
    return pageQuery;
}

function setMaxPages(numberOfItems) {
    maxPage = Math.ceil(numberOfItems / pageSize);
}

function setCurrentPage(page) {
    currnet = page;
}
