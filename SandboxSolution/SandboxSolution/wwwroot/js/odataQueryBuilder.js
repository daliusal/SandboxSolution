var filterCount = 0;

function addFilter(operators, columns) {
    filtersCount++;
    let filtersContainer = document.getElementById("filters");
    let filter = document.createElement("div");
    filter.id = "filter-" + filtersCount;

    let columnSelect = document.createElement("select");
    columnSelect.id = "filter-column-" + filtersCount;
    for (const col of columns) {
        columnSelect.innerHTML += "<option value=" + col + ">" + col + "</option>";
    }

    /*let logOperatorsSelect = document.createElement("select");
    logOperatorsSelect.id = "filter-log-operator-" + filtersCount;
    logOperatorsSelect.innerHTML = '<option value="and">And</option>'+
                                   '<option value="or">Or</option>' +
                                   '<option value="not">Not</option>'*/

    let operatorsSelect = document.createElement("select");
    operatorsSelect.id = "filter-operator-" + filtersCount;
    for (const [key, value] of Object.entries(operators)) {
        operatorsSelect.innerHTML += "<option value=" + value + ">" + key + "</option>";
    }

    let value = document.createElement("input");
    value.id = "filter-value-" + filtersCount;

    filter.appendChild(columnSelect);
    filter.innerHTML += " ";
    /*filter.appendChild(logOperatorsSelect);
    filter.innerHTML += " ";*/
    filter.appendChild(operatorsSelect);
    filter.innerHTML += " ";
    filter.appendChild(value);

    filtersContainer.appendChild(filter);
}

function buildQuery() {
    let filter = getFilter();
    console.log(filter)
    filter = filter.replace(/ and $/g, '');
    return filter;
}

function getFilter() {
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

        /*if (filter[column] == null) {
            filter[column] = [];
        }
        if (filter[column][operator] == null) {
            filter[column][operator] = [];
            filter[column][operator].push(value);
        }*/
    }

    return filter;
}
