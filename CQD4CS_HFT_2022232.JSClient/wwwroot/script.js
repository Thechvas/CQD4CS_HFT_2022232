let artists = [];
let festivals = [];
let songs = [];
getdata();
getdataf();
getdatas();

async function getdata() {
    await fetch('http://localhost:36286/artist')
        .then(x => x.json())
        .then(y => {
            artists = y;
            console.log(artists);
            display();
        });
}
async function getdataf() {
    await fetch('http://localhost:36286/festival')
        .then(x => x.json())
        .then(y => {
            festivals = y;
            console.log(festivals);
            displayf();
        });
}
async function getdatas() {
    await fetch('http://localhost:36286/song')
        .then(x => x.json())
        .then(y => {
            songs = y;
            console.log(songs);
            displays();
        });
}


function display() {
    document.getElementById('resultarea').innerHTML = "";
    artists.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.name + "</td><td>" +
            + t.numOfAlbums + "</td><td>" +
            + t.festivalId + "</td><td>" +
            `<button type="button" onclick="remove(${t.id})">Delete</button>`
            + "</td></tr>";
    });
}
function displayf() {
    document.getElementById('resultareaf').innerHTML = "";
    festivals.forEach(t => {
        document.getElementById('resultareaf').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.name + "</td><td>" +
            + t.location + "</td><td>" +
            + t.duration + "</td><td>" +
            `<button type="button" onclick="removef(${t.id})">Delete</button>`
            + "</td></tr>";
    });
}
function displays() {
    document.getElementById('resultareas').innerHTML = "";
    songs.forEach(t => {
        document.getElementById('resultareas').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.title + "</td><td>" +
            + t.artistId + "</td><td>" +
            + t.length + "</td><td>" +
            + t.genre + "</td><td>" +
            `<button type="button" onclick="removes(${t.id})">Delete</button>`
            + "</td></tr>";
    });
}


function remove(id){
    fetch('http://localhost:36286/artist/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
function removef(id) {
    fetch('http://localhost:36286/festival/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdataf();
        })
        .catch((error) => { console.error('Error:', error); });
}
function removes(id) {
    fetch('http://localhost:36286/song/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdatas();
        })
        .catch((error) => { console.error('Error:', error); });
}

function create() {
    let name = document.getElementById('artistname').value;
    let numOfAlbums = document.getElementById('numofalbums').value;
    let festivalId = document.getElementById('festivalid').value;
    fetch('http://localhost:36286/artist', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                name: name,
                numOfAlbums: numOfAlbums,
                festivalId: festivalId
            })
    })
        .then(response => response)
        .then(data =>
        {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
function createf() {
    let name = document.getElementById('festivalname').value;
    let location = document.getElementById('location').value;
    let duration = document.getElementById('duration').value;
    fetch('http://localhost:36286/festival', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                name: name,
                location: location,
                duration: duration
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdataf();
        })
        .catch((error) => { console.error('Error:', error); });
}
function creates() {
    let title = document.getElementById('songtitle').value;
    let genre = document.getElementById('songgenre').value;
    let length = document.getElementById('songlength').value;
    let artistId = document.getElementById('artistid').value;
    fetch('http://localhost:36286/song', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                title: title,
                genre: genre,
                length: length,
                artistId: artistId
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdatas();
        })
        .catch((error) => { console.error('Error:', error); });
}