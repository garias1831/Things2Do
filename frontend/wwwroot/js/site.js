/*Global user data object*/ 
const user = userSetup();

/*Sets up the user object representing all the data*/
function userSetup() {
    const user = {
        lat: 35.156038923689195,
        lng: -90.05194020924256,
        //The following initialized by mapsetup fn
        locationMarker: undefined, //Draggable location pin
        activePlaceMarkers: [], //Leaflet marker objects for place rsults
        map: undefined //NOTE - might b a better way here
    };
    return user;
}

/*Configures the inital map state and options*/
function mapSetup() {
    //Initial map + home marker position
    const initX = user.lat;
    const initY = user.lng;

    //So user can't wrap around world + see gray zones at poles
    const bounds = [
        [-88, -180],
        [88, 180],
    ];
    const map = L.map('map', {
        maxZoom : 17, 
        maxBounds: bounds,
        maxBoundsViscosity: 1.0,
        zoomControl : false}).setView([initX, initY], 13);
    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
                minZoom: 3,
                attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
             }).addTo(map);

    //NOTE -- will have to put this somewhere accessible (to get things like location )
    const homemarker = L.marker([initX, initY], {
        alt: 'search location',
        draggable: true, 
        autoPan: true}).addTo(map);

    user.locationMarker = homemarker;
    return map;
}

/*Updates the search location to the user's current position*/
//TODO -- might wanna store user location in a cookie / localstorage 4 future visits
function getUserLocation() {
    if(!('geolocation' in navigator)){
        alert('Geolocation services are not available in your browser.');
        return;
    }
    
    //TODO -- might be nice to add some loading flavortext, as it looks like
    //the geolocation api could take a bit sometimes
    function success(pos) {
        const coords = pos.coords;
        user.lat = coords.latitude;
        user.lng = coords.longitude;
        
        //Move the home icon and pan the map round
        user.locationMarker.setLatLng([user.lat, user.lng]);
        user.map.panTo([user.lat, user.lng])
    }

    function fail(error) {
        console.warn(`ERROR(${error.code}): ${error.message}`);

        //TODO -- might be nice if this prompt was a styled css bubble instead of an alert
        if (error.code === 1) {
            alert('Geolocation denied by user. Please refresh the page to enable geolocation.');
            return;
        }

        alert('There was an error with the geolocation.');
    }

    navigator.geolocation.getCurrentPosition(success, fail);
}

function setPlaceMarkers(places) {
    console.log(places);
    //TODO -- would b nice to make a custom popup displaying the title of the place w/ css
    //Kinda like how google maps does
    places.forEach(place => {
        const pos = place.position;
        const marker = L.marker([pos.lat, pos.lng], {
            title: place.title
        });

        user.activePlaceMarkers.push(marker);
        marker.addTo(user.map);

    });
    
}

async function search() {
    
    try { //TODO -- replace w/ actual backend location
        const response = await fetch("http://localhost:5057/search", {
            method: "POST",
            headers: {
                "Accept" : "application/json",
                "Content-Type" : "application/json"
            },
            body: JSON.stringify({
                lat: user.lat, 
                lng: user.lng
            })
        });

        const placeResults = await response.json();
        setPlaceMarkers(placeResults);

    } catch (error) {
        console.warn(error.message);
        //Need to warn the client
    }
}

//Run setup functions + bind callbacks

//Initialise the map for the given session
user.map = mapSetup();

const geolocateBtn = document.getElementById('geolocate');
const searchBtn = document.getElementById('search');

geolocateBtn.addEventListener('click', () => getUserLocation());
searchBtn.addEventListener('click',  () => search());

