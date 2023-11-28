let artists = [];
let festivals = [];
let songs = [];
let connection = null;
let artistIdToUpdate = -1;
let festivalIdToUpdate = -1;
let songIdToUpdate = -1;
getdata();
getdataf();
getdatas();
setupSignalR();


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:36286/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("ArtistCreated", (user, message) => {
        getdata();
    });

    connection.on("ArtistDeleted", (user, message) => {
        getdata();
    });

    connection.on("ArtistUpdated", (user, message) => {
        getdata();
    });

    connection.on("SongCreated", (user, message) => {
        getdatas();
    });

    connection.on("SongDeleted", (user, message) => {
        getdatas();
    });

    connection.on("SongUpdated", (user, message) => {
        getdatas();
    });

    connection.on("FestivalCreated", (user, message) => {
        getdataf();
    });

    connection.on("FestivalDeleted", (user, message) => {
        getdataf();
    });

    connection.on("FestivalUpdated", (user, message) => {
        getdataf();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

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
            "<tr><td>" + t.id + "</td><td>" + t.name + "</td><td>" + t.numOfAlbums + "</td><td>" + t.festivalId + "</td><td>" +
            `<button type="button" onclick="remove(${t.id})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.id})">Update</button>`
            + "</td></tr>";
    });
}
function displayf() {
    document.getElementById('resultareaf').innerHTML = "";
    festivals.forEach(t => {
        
        console.log("Location: ", t.location); // Ensure location values are correct

        document.getElementById('resultareaf').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.location + "</td><td>"
            + t.name + "</td><td>" +
            + t.duration + "</td><td>" +
            `<button type="button" onclick="removef(${t.id})">Delete</button>` +
            `<button type="button" onclick="showupdatef(${t.id})">Update</button>`
            + "</td></tr>";
    });
}
function displays() {
    document.getElementById('resultareas').innerHTML = "";
    songs.forEach(t => {
        document.getElementById('resultareas').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.title + "</td><td>" + t.artistId + "</td><td>" + t.length + "</td><td>" + t.genre + "</td><td>" +
            `<button type="button" onclick="removes(${t.id})">Delete</button>` +
            `<button type="button" onclick="showupdates(${t.id})">Update</button>`
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

function showupdate(id) {
    document.getElementById('artistnametoupdate').value = artists.find(t => t['id'] == id)['name'];
    document.getElementById('numofalbumstoupdate').value = artists.find(t => t['id'] == id)['numOfAlbums'];
    document.getElementById('festivalidtoupdate').value = artists.find(t => t['id'] == id)['festivalId'];
    document.getElementById('updateformdiv').style.display = 'flex';
    artistIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('artistnametoupdate').value;
    let numOfAlbums = document.getElementById('numofalbumstoupdate').value;
    let festivalId = document.getElementById('festivalidtoupdate').value;

    fetch('http://localhost:36286/artist', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                name: name,
                numOfAlbums: numOfAlbums,
                festivalId: festivalId,
                id: artistIdToUpdate
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function showupdatef(id) {
    document.getElementById('festivalnametoupdate').value = festivals.find(t => t['id'] == id)['name'];
    document.getElementById('locationtoupdate').value = festivals.find(t => t['id'] == id)['location'];
    document.getElementById('durationtoupdate').value = festivals.find(t => t['id'] == id)['duration'];
    document.getElementById('updatefestdiv').style.display = 'flex';
    festivalIdToUpdate = id;
}

function updatef() {
    document.getElementById('updatefestdiv').style.display = 'none';
    let name = document.getElementById('festivalnametoupdate').value;
    let location = document.getElementById('locationtoupdate').value;
    let duration = document.getElementById('durationtoupdate').value;

    fetch('http://localhost:36286/festival', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                name: name,
                location: location,
                duration: duration,
                id: festivalIdToUpdate
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdataf();
        })
        .catch((error) => { console.error('Error:', error); });
}

function showupdates(id) {
    document.getElementById('songtitletoupdate').value = songs.find(t => t['id'] == id)['title'];
    document.getElementById('artistidtoupdate').value = songs.find(t => t['id'] == id)['artistId'];
    document.getElementById('songlengthtoupdate').value = songs.find(t => t['id'] == id)['length'];
    document.getElementById('songgenretoupdate').value = songs.find(t => t['id'] == id)['genre'];
    document.getElementById('updatesongdiv').style.display = 'flex';
    songIdToUpdate = id;
}

function updates() {
    document.getElementById('updatesongdiv').style.display = 'none';
    let title = document.getElementById('songtitletoupdate').value;
    let artistId = document.getElementById('artistidtoupdate').value;
    let length = document.getElementById('songlengthtoupdate').value;
    let genre = document.getElementById('songgenretoupdate').value;

    fetch('http://localhost:36286/song', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                title: title,
                artistId: artistId,
                length: length,
                genre: genre,
                id: songIdToUpdate
            })
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