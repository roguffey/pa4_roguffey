

const baseUrl = "https://localhost:5001/api/Songs"
var songList = [];
var mySong = {};

function handleOnLoad(){
    populateList();
}

function GetSongs() {
    const allSongsAPIURL = baseUrl;
    
    fetch(allSongsAPIURL).then(function(response){
        console.log(response);
        return response.json();
    }).then(function(json){
        songList = json;
        let html = ``;
        json.forEach((song) => {
            console.log(song.songTitle)
            html += `<div class="card col-md-4 bg-dark text-white">`;
			html += `<img src="./images/music.jpeg" class="card-img" alt="...">`;
			html += `<div class="card-img-overlay">`;
			html += `<h5 class="card-title">`+song.songTitle+`</h5>`;
            if (song.favorite == "n"){
                html += '<button id= '+song.songID+' class="btn btn-success" onclick="favorite(id)">Favorite</button>';
                html += '<button id= '+song.songID+' class="btn btn-danger" onclick="DeleteSong(id)">Delete</button>';
            }
            else if(song.favorite == "y"){
                html += '<button id= '+song.songID+' class="btn btn-warning" id = "selectId" onclick="unfavorite(id)">Unfavorite</button>';
            }
            html += `</div>`;
            html += `</div>`;
        });
        
        document.getElementById("songTitle").innerHTML = html;
        
    }).catch(function(error){
        console.log(error);
    });
}

function postSong() {
    const postSongsAPIURL ="https://localhost:5001/api/Songs";
    console.log(postSongsAPIURL);
    const songText = document.getElementById("addSong").value;
    
    fetch(postSongsAPIURL, {
        method: "Post",
        headers:{
            "Accept": 'application/json',
            "Content-Type": 'application/json',
        },
        body: JSON.stringify({songTitle : songText})
    })
    .then((response)=>{
        console.log(response);
        GetSongs();
    })
}

function EditSong(id, pt) {
    const editSongAPIURL = baseUrl+ " /" + id;
    
    fetch(editPostAPIURL, {
        method: "POST",
        headers:{
            "Accept": 'application/json',
            "Content-Type": 'application/json'
        },
        body: JSON.stringify({
            SongTitle: st,
        })
    }).then((response)=>{
        console.log(response);
        GetSongs();
    })
}


function DeleteSong(id) {
    const deleteSongAPIURL = baseUrl+ "/" + id;
    fetch(deleteSongAPIURL, {
        method: "DELETE",
        headers:{
            "Accept": 'application/json',
            "Content-Type": 'application/json'
        },
    }).then((response)=>{
        console.log(response);
        GetSongs();
    })
}


function findSongs(){
    var url = "https://www.songsterr.com/a/ra/songs.json?pattern="
    let searchString = document.getElementById("searchSong").value;

    url += searchString;

    console.log(searchString)

    fetch(url).then(function(response) {
		console.log(response);
		return response.json();
	}).then(function(json) {
        console.log(json)
        let html = ``;
		json.forEach((song) => {
            console.log(song.title)
            html += `<div class="card col-md-4 bg-dark text-white">`;
			html += `<img src="./images/music.jpeg" class="card-img" alt="...">`;
			html += `<div class="card-img-overlay">`;
			html += `<h5 class="card-title">`+song.title+`</h5>`;
            html += `</div>`;
            html += `</div>`;
		});
		
        if(html === ``){
            html = "No Songs found :("
        }
		document.getElementById("searchSongs").innerHTML = html;

	}).catch(function(error) {
		console.log(error);
	})
}
function favorite(id){
    const url = baseUrl + "/" + id;
    const Deleted = "n";
    const Favorite = "y";

    const sendSong = {
        deleted: Deleted,
        favorite: Favorite,
    }

    fetch(url, {
        method: "PUT",
        headers: {
            "Accept": 'application/json',
            "Content-Type": 'application/json',
        },
        body: JSON.stringify(sendSong)
    }).then((response)=>{
        GetSongs();
    })
}
function unfavorite(id){
    const url = baseUrl + "/" + id;;
    const Deleted = "n";
    const Favorite = "n";

    const sendSong = {
        deleted: Deleted,
        favorite: Favorite,
    }

    fetch(url, {
        method: "PUT",
        headers: {
            "Accept": 'application/json',
            "Content-Type": 'application/json',
        },
        body: JSON.stringify(sendSong)
    }).then((response)=>{
        GetSongs();
    })
}