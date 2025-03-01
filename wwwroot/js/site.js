const availableToRandom = new Array("Starts", "BaseLocations", "Goals", "Restrictions")

function getRandomResult(name) {
    var uri = window.location.href + `GetRandomResult?name=${name}`
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayItems(data, name))
        .catch(error => console.error('Unable to get items.', error));
}

function _displayItems(data, name) {
    var result = document.getElementById(name)
    if (data.length > 1) {
        var desc = document.getElementById(name+'Desc')
        desc.innerHTML = data[1]
    }
    result.innerHTML = data[0]
}

function getRandomResultForAll() {
    availableToRandom.forEach(name => getRandomResult(name))
}

