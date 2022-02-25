const apiBase = "https://localhost:7077/api";

function onClickSubmit() {
    if (document.forms["new-publisher-form"].elements["publisher-name"].value == "") {
        alert("Publisher name cannot be empty");
        return;
    }
    postData(apiBase + "/Publisher");
}
async function postData(url) {
    let data = {
        name: document.forms["new-publisher-form"].elements["publisher-name"].value
    };

    const response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    }).then(function () {
        location.href = "/admin/publishers"
    });
}