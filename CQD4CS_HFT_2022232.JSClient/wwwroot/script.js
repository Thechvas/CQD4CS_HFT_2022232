let actors = [];
getdata();

async function getdata() {
    await fetch('http://localhost:36286/artist')
        .then(x => x.json())
        .then(y => {
            actors = y;
            console.log(actors);
            display();
        });
}

function display() {
    actors.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.name + "</td></tr>";

        console.log(t.name);
    });
}

function create() {
    let name = document.getElementById('artistname').value;
    fetch('http://localhost:36286/artist', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                artistname: name,

            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}