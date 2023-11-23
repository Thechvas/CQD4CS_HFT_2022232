let actors = [];

fetch('http://localhost:36286/artist')
    .then(x => x.json())
    .then(y => {
        actors = y;
        console.log(actors);
        display();
    });

function display() {
    actors.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.name + "</td></tr>";

        console.log(t.name);
    });
}