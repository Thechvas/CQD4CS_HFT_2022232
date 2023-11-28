let artists = [];
let festivals = [];
let songs = [];
let connection = null;
let artistIdToUpdate = -1;
let festivalIdToUpdate = -1;
let songIdToUpdate = -1;
let festivalWithMostArtists = null;
let longestSongOfArtist = null;
let selectedArtist = null;
let selectedLocation = null;
let artistWithMostAlbums = null;
let selectedFestival = null;
let totalDurationOfFestival = null;
let selectedSpecificArtist = null;
let selectedSpecificGenre = null;
let specificSong = null;


getdata();
getdataf();
getdatas();
getNonCrudData();
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
            //console.log(artists);
            display();
        });
}
async function getdataf() {
    await fetch('http://localhost:36286/festival')
        .then(x => x.json())
        .then(y => {
            festivals = y;
            //console.log(festivals);
            displayf();
        });
}
async function getdatas() {
    await fetch('http://localhost:36286/song')
        .then(x => x.json())
        .then(y => {
            songs = y;
            //console.log(songs);
            displays();
        });
}

async function getNonCrudData() {
    await fetch('http://localhost:36286/Stat/FestivalWithMostArtists', {
        method: 'GET',
        headers: {
            'Content-Type': 'text/plain',
        },
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok.');
            }
            return response.text();
        })
        .then(data => {
            festivalWithMostArtists = data;
            displayNonCrud();
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
            
        });

    await fetch('http://localhost:36286/Stat/LongestSongOfArtist?artistName=' + selectedArtist, {
        method: 'GET',
        headers: {
            'Content-Type': 'text/plain',
        },
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok.');
            }
            return response.text();
        })
        .then(data => {
            longestSongOfArtist = data;

            displayNonCrud();
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);

        });

    await fetch('http://localhost:36286/Stat/ArtistWithMostAlbums?festivalLocation=' + selectedLocation, {
        method: 'GET',
        headers: {
            'Content-Type': 'text/plain',
        },
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok.');
            }
            return response.text();
        })
        .then(data => {
            artistWithMostAlbums = data;

            displayNonCrud();
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);

        });

    await fetch('http://localhost:36286/Stat/TotalDurationOfFestival?festivalId=' + selectedFestival, {
        method: 'GET',
        headers: {
            'Content-Type': 'text/plain',
        },
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok.');
            }
            return response.text();
        })
        .then(data => {
            totalDurationOfFestival = data;

            displayNonCrud();
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);

        });

    await fetch('http://localhost:36286/Stat/SpecificSongFinder?artistName=' + selectedSpecificArtist + '&genreName=' + selectedSpecificGenre, {
        method: 'GET',
        headers: {
            'Content-Type': 'text/plain',
        },
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok.');
            }
            return response.text();
        })
        .then(data => {
            specificSong = data;

            displayNonCrud();
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);

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
        document.getElementById('resultareaf').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.name + "</td><td>"
            + t.location + "</td><td>" +
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

function displayNonCrud() {
    document.getElementById('festivalWithMostArtists').innerHTML = "Festival with Most Artists: " + festivalWithMostArtists;

    let artistSelect = document.getElementById('artistSelect');
    artistSelect.innerHTML = '<option value="">Select an artist</option>';

    artists.forEach(artist => {
        artistSelect.innerHTML +=
            `<option value="${artist.name}">${artist.name}</option>`;
    })

    artistSelect.addEventListener('change', async (event) => {
        selectedArtist = event.target.value;

        try {
            const response = await fetch('http://localhost:36286/Stat/LongestSongOfArtist?artistName=' + selectedArtist, {
                method: 'GET',
                headers: {
                    'Content-Type': 'text/plain',
                },
            });

            if (!response.ok) {
                throw new Error('error');
            }

            const longestSong = await response.text();
            document.getElementById('longestSongOfArtist').innerHTML = longestSong;
        } catch (error) {
            console.error('error:', error);
        }

    });
    let locationSelect = document.getElementById('locationSelect');
    locationSelect.innerHTML = '<option value="">Select a location</option>';

    festivals.forEach(festival => {
        locationSelect.innerHTML +=
            `<option value="${festival.location}">${festival.location}</option>`;
    })

    locationSelect.addEventListener('change', async (event) => {
        selectedLocation = event.target.value;

        try {
            const response = await fetch('http://localhost:36286/Stat/ArtistWithMostAlbums?festivalLocation=' + selectedLocation, {
                method: 'GET',
                headers: {
                    'Content-Type': 'text/plain',
                },
            });

            if (!response.ok) {
                throw new Error('error');
            }

            const artitswma = await response.text();
            document.getElementById('artistWithMostAlbums').innerHTML = artitswma;
        } catch (error) {
            console.error('error:', error);
        }
    });

    let festivalSelect = document.getElementById('festivalSelect');
    festivalSelect.innerHTML = '<option value="">Select a festival</option>';

    festivals.forEach(festival => {
        festivalSelect.innerHTML +=
            `<option value="${festival.id}">${festival.name}</option>`;
    })

    festivalSelect.addEventListener('change', async (event) => {
        selectedFestival = event.target.value;

        try {
            const response = await fetch('http://localhost:36286/Stat/TotalDurationOfFestival?festivalId=' + selectedFestival, {
                method: 'GET',
                headers: {
                    'Content-Type': 'text/plain',
                },
            });

            if (!response.ok) {
                throw new Error('error');
            }

            const totaldof = await response.text();
            document.getElementById('totalDurationOfFestival').innerHTML = totaldof;
        } catch (error) {
            console.error('error:', error);
        }
    });

    let specificArtistSelect = document.getElementById('specificArtist');
    specificArtistSelect.innerHTML = '<option value="">Select an artist</option>';

    artists.forEach(artist => {
        specificArtistSelect.innerHTML +=
            `<option value="${artist.name}">${artist.name}</option>`;
    })

    let specificGenreSelect = document.getElementById('specificGenre');
    specificGenreSelect.addEventListener('change', async (event) => {
        selectedSpecificGenre = event.target.value;

        try {
            const response = await fetch('http://localhost:36286/Stat/SpecificSongFinder?artistName=' + selectedSpecificArtist + '&genreName=' + selectedSpecificGenre, {
                method: 'GET',
                headers: {
                    'Content-Type': 'text/plain',
                },
            });

            if (!response.ok) {
                throw new Error('error');
            }

            const spsong = await response.text();
            document.getElementById('specificSong').innerHTML = spsong;
        } catch (error) {
            console.error('error:', error);
        }
    });

    specificArtistSelect.addEventListener('change', async (event) => {
        selectedSpecificArtist = event.target.value;

        try {
            const response = await fetch('http://localhost:36286/Stat/SpecificSongFinder?artistName=' + selectedSpecificArtist + '&genreName=' + selectedSpecificGenre, {
                method: 'GET',
                headers: {
                    'Content-Type': 'text/plain',
                },
            });

            if (!response.ok) {
                throw new Error('error');
            }

            const spsong = await response.text();
            document.getElementById('specificSong').innerHTML = spsong;
        } catch (error) {
            console.error('error:', error);
        }
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
            getNonCrudData();
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
            getNonCrudData();
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
            getNonCrudData();

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
            getNonCrudData();

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
            getNonCrudData();
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
            getNonCrudData();

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
            getNonCrudData();

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
            getNonCrudData();
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
            getNonCrudData();

        })
        .catch((error) => { console.error('Error:', error); });
}